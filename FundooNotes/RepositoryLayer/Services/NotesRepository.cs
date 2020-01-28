//-----------------------------------------------------------------------
// <copyright file="NotesRepository.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Services
{
    using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="FundooRepositoryLayer.Interfaces.INotesRepository" />
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private UserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public NotesRepository(UserContext userContext)
        {
            this._userContext = userContext;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">user Id</param>
        /// <returns>
        /// returns the added note
        /// </returns>
        /// <exception cref="System.Exception">returns exception</exception>
        public async Task<NoteResponseModel> AddNotes(NotesRequestModel notesModel, int userId)
        {
            try
            {
                NotesModel model = new NotesModel()
                {

                    ID = userId,
                    Title = notesModel.Title,
                    Description = notesModel.Description,
                    Reminder = notesModel.Reminder,
                    IsCreated = DateTime.Now,
                    IsModified =DateTime.Now,
                    IsPin=notesModel.IsPin,
                    IsArchive=notesModel.IsArchive
                
                };
              
                this._userContext.Notes.Add(model);
                await _userContext.SaveChangesAsync();
                if (notesModel != null && notesModel.labels.Count != 0)
                {
                    List<RequestNotesLabels> requestNotesLabels = notesModel.labels;
                    foreach (RequestNotesLabels request in requestNotesLabels)
                    {
                        if (request.Id > 0)
                        {
                            var data = new LabelsNotes()
                            {
                                LabelId = request.Id,
                                NoteId = model.ID

                            };
                            _userContext.labelsNotes.Add(data);
                            await _userContext.SaveChangesAsync();
                        }
                    }
                }
                List<LabelResponseModel> labelResponseModels = _userContext.labelsNotes.Join(_userContext.Labels,
                    label => label.LabelId,
                    note => note.Id,
                    (note, label) => new LabelResponseModel
                    {
                        Id = label.Id,
                        Label = label.Label,
                        IsCreated = label.IsCreated,
                        IsModified = label.IsModified
                    }).ToList() ;

                NoteResponseModel noteResponse = new NoteResponseModel()
                {
                    Id = model.ID,
                    Title = model.Title,
                    Description = model.Description,
                    Reminder = model.Reminder,
                    IsCreated = model.IsCreated,
                    IsModified = model.IsModified,
                    IsPin = model.IsPin,
                    IsArchive = model.IsArchive,
                    Color = model.Color,
                    Image = model.Image,
                    labels = labelResponseModels
                };
                return noteResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NoteResponseModel> GetAllNotes(int userId)
        {
            try
            {
                List<NoteResponseModel> notes = _userContext.Notes.Where(linq => linq.ID == userId).Select(linq => new NoteResponseModel
                {
                    Id = linq.ID,
                    Title = linq.Title,
                    Description = linq.Description,
                    Reminder = linq.Reminder,
                    Image = linq.Image,
                    IsArchive = linq.IsArchive,
                    IsPin =linq.IsPin,
                    IsTrash=linq.IsTrash,
                    IsCreated=linq.IsCreated,
                    IsModified=linq.IsModified
                  }).ToList();
                if (notes.Count != 0 && notes != null)
                {
                    foreach (NoteResponseModel noteResponse in notes)
                    {
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.Id == noteResponse.Id).Join(_userContext.Labels,
                            label => label.LabelId,
                            note => note.Id, 
                            (label, note) => new LabelResponseModel
                            {
                                Id=label.LabelId,
                                Label=note.Label,
                                IsCreated=note.IsCreated,
                                IsModified=note.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;
                    }
                }
               
                    return notes;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<NoteResponseModel> UpdateNotes(NotesRequestModel notesModel,int noteId, int userId)
        {
            try
            {
                var notes = _userContext.Notes.FirstOrDefault(linq => linq.ID == userId && linq.NotesID == noteId);
                NotesModel notesModel1 = new NotesModel();
                if (notes != null)
                {
                    notes.Title = notesModel.Title;
                    notes.Description = notesModel.Description;
                    notes.IsModified = DateTime.Now;
                    notes.Reminder = DateTime.Now;
                    notes.Color = notesModel.Colour;
                    notes.Image = notesModel.Image;
                    var note = this._userContext.Notes.Attach(notes);
                    note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                 
                }
                NoteResponseModel noteResponse = new NoteResponseModel()
                {
                    Id = noteId,
                    Title = notesModel1.Title,
                    Description = notesModel1.Description,
                    Reminder = notesModel1.Reminder,
                    Image = notesModel1.Image,
                    Color = notesModel1.Color,
                    IsCreated = notesModel1.IsCreated,
                    IsModified = notesModel1.IsModified,
                    IsPin = notesModel1.IsPin,
                    IsArchive = notesModel1.IsArchive,
                    IsTrash = notesModel1.IsTrash
                };

               
                    return noteResponse;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteNote(int userId,int notesId)
        {
            try
            {
                NotesModel note = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == notesId));
                if (note != null)
                {
                    if (note.IsTrash == true)
                    {
                        _userContext.Notes.Remove(note);
                        await this._userContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public NoteResponseModel GetNote(int userId, int noteId)
        {
            try
            {
                NotesModel notes = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.NotesID == noteId)).FirstOrDefault();
                List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.Id == noteId).Join(_userContext.Labels,
                    label => label.LabelId,
                    note => note.Id,
                    (label, note) => new LabelResponseModel
                    {
                        Id = label.LabelId,
                        Label = note.Label,
                        IsCreated = note.IsCreated,
                        IsModified = note.IsModified
                    }).ToList();
                NoteResponseModel noteResponse = _userContext.Notes.Where(c => (c.NotesID == noteId) && (c.ID == userId)).Select(c => new NoteResponseModel
                {
                    Id = c.ID,
                    Title = c.Title,
                    Description = c.Description,
                    Reminder = c.Reminder,
                    Image = c.Image,
                    IsArchive = c.IsArchive,
                    IsTrash = c.IsTrash,
                    IsPin = c.IsPin,
                    IsCreated = c.IsCreated,
                    IsModified = c.IsModified,
                    labels = labelResponses
                }).FirstOrDefault();
                return noteResponse;
            }
           
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> IsTrash(int UserId, int noteId)
        {
            try
            {
                bool flag = false;
                NotesModel notes = _userContext.Notes.FirstOrDefault(linq => (linq.NotesID == noteId) && (linq.ID == UserId));
                if (notes != null)
                {
                    if (!notes.IsTrash)
                    {
                        notes.IsTrash = true;
                        var note = this._userContext.Notes.Attach(notes);
                        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await this._userContext.SaveChangesAsync();
                        flag = true;
                    }
                    else
                    {
                        notes.IsTrash = false;
                        var note = this._userContext.Notes.Attach(notes);
                        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await this._userContext.SaveChangesAsync();
                        flag = true;
                    }

                }
                return flag;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NoteResponseModel> GetAllTrash(int userId)
        {
            try
            {
                List<NoteResponseModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == true) && (linq.IsArchive == false) && (linq.IsPin == false)).Select
                    (linq => new NoteResponseModel
                    {
                        Id = linq.ID,
                        Title = linq.Title,
                        Description = linq.Description,
                        Reminder = linq.Reminder,
                        Image = linq.Image,
                        IsArchive = linq.IsArchive,
                        IsPin = linq.IsPin,
                        IsTrash = linq.IsTrash,
                        IsCreated = linq.IsCreated,
                        IsModified = linq.IsModified
                    }).ToList();
                if (notesModels.Count != 0)
                {
                    return notesModels;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> IsPin(int userId, int noteId)
        {
            try
            {
                bool flag = false;
                NotesModel notesModel = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == noteId));
                if (notesModel != null)
                {
                    if (!notesModel.IsPin)
                    {
                        notesModel.IsPin = true;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        flag = true;

                    }
                    else
                    {
                        notesModel.IsPin = false;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> IsArchive(int userId, int noteId)
        {
            try
            {
                bool flag = false;
                NotesModel notesModel = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == noteId));
                if (notesModel != null)
                {
                    if (!notesModel.IsArchive)
                    {
                        notesModel.IsArchive = true;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        flag = true;

                    }
                    else
                    {
                        notesModel.IsArchive = false;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NoteResponseModel> GetAllPin(int userId)
        {
            try
            {
                List<NoteResponseModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == false) && (linq.IsArchive == false) && (linq.IsPin == true)).Select
                    (linq => new NoteResponseModel
                    {
                        Id = linq.ID,
                        Title = linq.Title,
                        Description = linq.Description,
                        Reminder = linq.Reminder,
                        Image = linq.Image,
                        IsArchive = linq.IsArchive,
                        IsPin = linq.IsPin,
                        IsTrash = linq.IsTrash,
                        IsCreated = linq.IsCreated,
                        IsModified = linq.IsModified
                    }).ToList();
                if (notesModels.Count != 0)
                {
                    return notesModels;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NoteResponseModel> GetAllArchive(int userId)
        {
            try
            {
                List<NoteResponseModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == false) && (linq.IsArchive == true) && (linq.IsPin == false)).Select
                (linq => new NoteResponseModel
                {
                    Id = linq.ID,
                    Title = linq.Title,
                    Description = linq.Description,
                    Reminder = linq.Reminder,
                    Image = linq.Image,
                    IsArchive = linq.IsArchive,
                    IsPin = linq.IsPin,
                    IsTrash = linq.IsTrash,
                    IsCreated = linq.IsCreated,
                    IsModified = linq.IsModified
                }).ToList();
                if (notesModels.Count != 0)
                {
                    return notesModels;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteAllTrash(int userId)
        {
            try
            {
                List<NotesModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == true) && (linq.IsArchive == false) && (linq.IsPin == false)).ToList();
                if (notesModels.Count != 0)
                {
                    _userContext.Notes.RemoveRange(notesModels);
                    await _userContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NoteResponseModel> GetNoteByLabelId(int labelId)
        {
            try
            {
                List<NoteResponseModel> noteResponses = _userContext.labelsNotes.Where(linq => linq.Id == labelId).Join(_userContext.Notes,
                    label => label.NoteId,
                    note => note.NotesID,
                    (note, label) => new NoteResponseModel
                    {
                        Id = note.Id,
                        Title = label.Title,
                        Description = label.Description,
                        Color = label.Color,
                        Image = label.Image,
                        IsPin = label.IsPin,
                        IsArchive = label.IsArchive,
                        IsCreated = label.IsCreated,
                        IsModified = label.IsModified,
                        IsTrash = label.IsTrash
                    }).ToList();
                if (noteResponses.Count != 0)
                {
                    return noteResponses;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
