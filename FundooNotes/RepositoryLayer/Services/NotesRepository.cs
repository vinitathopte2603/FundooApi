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
                    Image = string.IsNullOrWhiteSpace(notesModel.Image)? "null": ImageUploadCloudinary.AddPhoto(notesModel.Image),
                    Color = string.IsNullOrWhiteSpace(notesModel.Colour) ? "null" : notesModel.Colour,
                    IsCreated = DateTime.Now,
                    IsModified = DateTime.Now,
                    IsPin = notesModel.IsPin,
                    IsArchive = notesModel.IsArchive
                };
                
                    this._userContext.Notes.Add(model);
                    await _userContext.SaveChangesAsync();
                //// Adding labels to the note
                    if (notesModel != null && notesModel.labels.Count != 0)
                    {
                        List<RequestNotesLabels> requestNotesLabels = notesModel.labels;
                        foreach (RequestNotesLabels request in requestNotesLabels)
                        {
                        LabelModel label = _userContext.Labels.FirstOrDefault(linq => linq.UserId == userId && linq.Id == request.Id);
                            if (request.Id > 0 && label != null)
                            {
                                var data = new LabelsNotes()
                                {
                                    LabelId = request.Id,
                                    NoteId = model.NotesID

                                };
                                _userContext.labelsNotes.Add(data);
                                await _userContext.SaveChangesAsync();
                            }
                        }
                    }
                    List<LabelResponseModel> labelResponseModels = _userContext.labelsNotes
                        .Where(note => note.NoteId == model.NotesID)
                        .Join(_userContext.Labels,
                        notelabel => notelabel.LabelId,
                        label => label.Id,
                        (notelabel, label) => new LabelResponseModel
                        {
                            Id = label.Id,
                            Label = label.Label,
                            IsCreated = label.IsCreated,
                            IsModified = label.IsModified
                        }).ToList();
                //// Adding collaborator to the note
                if (notesModel != null && notesModel.Collaborators.Count != 0)
                {
                    List<CollaboratorRequestModel> collaboratorRequests = notesModel.Collaborators;
                    foreach (CollaboratorRequestModel requestModel in collaboratorRequests)
                    {
                        UserDB user = _userContext.Users.FirstOrDefault(linq => linq.Id == requestModel.UserId);
                        if (requestModel.UserId != 0 && user != null)
                        {
                            var data = new CollaborationModel()
                            {
                                NoteId = model.NotesID,
                                UserId = user.Id
                            };
                            _userContext.collaborations.Add(data);
                            await _userContext.SaveChangesAsync();
                        }
                    }
                }

                List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                      .Where(note => note.NoteId == model.NotesID)
                      .Join(_userContext.Users,
                      collab => collab.UserId,
                      user => user.Id,
                      (collab, user) => new CollaborationResponseModel
                      {
                        UserId = user.Id,
                        Email=user.Email,
                        Firstname= user.FirstName,
                        LastName = user.LastName
                      }).ToList();
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
                        labels = labelResponseModels,
                        collaborations =collaborationResponses
                    };
                return noteResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NoteResponseModel> GetAllNotes(int userId, string keyword)
        {
            try
            {
              


                List<NoteResponseModel> collabNotes = _userContext.collaborations.Where(linq => linq.UserId == userId)
                    .Join(_userContext.Notes,
                    collab => collab.NoteId,
                    notetable => notetable.NotesID,
                      (collab, notetable) => new NoteResponseModel
                      {
                          Title = notetable.Title,
                          Description = notetable.Description,
                          Reminder = notetable.Reminder,
                          Image = notetable.Image,
                          IsArchive = notetable.IsArchive,
                          IsPin = notetable.IsPin,
                          IsCreated = notetable.IsCreated,
                          IsModified = notetable.IsModified,
                          Id=notetable.NotesID  
                      }
                    ).ToList();

                foreach (var collab in collabNotes)
                {
                    List<CollaborationResponseModel> collaborationResponses = _userContext.Notes
                           .Where(note => note.NotesID == collab.Id)
                           .Join(_userContext.Users,
                           collabnote => collabnote.ID,
                           user => user.Id,
                           (collabnote, user) => new CollaborationResponseModel
                           {
                               UserId = user.Id,
                               Email = user.Email,
                               Firstname = user.FirstName,
                               LastName = user.LastName
                           }).ToList();

                    collab.collaborations = collaborationResponses;

                }



                List<NoteResponseModel> notes = _userContext.Notes.Where(linq => linq.ID == userId).Select(linq => new NoteResponseModel
                {
                    Id = linq.NotesID,
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
                if (notes.Count != 0 && notes != null)
                {
                    foreach (NoteResponseModel noteResponse in notes)
                    {
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.NoteId == noteResponse.Id).Join(_userContext.Labels,
                            labelnote => labelnote.LabelId,
                            label => label.Id,
                            (labelnote, label) => new LabelResponseModel
                            {
                                Id = labelnote.LabelId,
                                Label = label.Label,
                                IsCreated = label.IsCreated,
                                IsModified = label.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;

                        List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                            .Where(note => note.NoteId == noteResponse.Id)
                            .Join(_userContext.Users,
                            collab => collab.UserId,
                            user => user.Id,
                            (collab, user) => new CollaborationResponseModel
                            {
                                UserId = user.Id,
                                Email = user.Email,
                                Firstname = user.FirstName,
                                LastName = user.LastName
                            }).ToList();
                        noteResponse.collaborations = collaborationResponses;
                    }
                }
                notes.AddRange(collabNotes);
                if (keyword != null)
                {
                    List<NoteResponseModel> searchNoteResponses = SearchNote(userId, keyword);
                    return searchNoteResponses;
                }
                else
                {
                    if (notes.Count != 0)
                    {
                        return notes;
                    }
                    else
                    {
                        return null;
                    }
                }
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
                    notes.Reminder = notesModel.Reminder;
                    notes.Color = string.IsNullOrWhiteSpace(notesModel.Colour) ? "null" : notesModel.Colour;
                    notes.Image = string.IsNullOrWhiteSpace(notesModel.Image) ? "null" : ImageUploadCloudinary.AddPhoto(notesModel.Image);
                    var note = this._userContext.Notes.Attach(notes);
                    note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                 
                }

                NoteResponseModel noteResponse = _userContext.Notes.Where(c => (c.NotesID == noteId) && (c.ID == userId)).Select(c => new NoteResponseModel
                {
                    Id = c.NotesID,
                    Title = c.Title,
                    Description = c.Description,
                    Reminder = c.Reminder,
                    Image = c.Image,
                    IsArchive = c.IsArchive,
                    IsTrash = c.IsTrash,
                    IsPin = c.IsPin,
                    IsCreated = c.IsCreated,
                    IsModified = c.IsModified
                  
                }).FirstOrDefault();
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
                    List<LabelsNotes> labelsnotes = _userContext.labelsNotes.Where(linq => linq.NoteId == notesId).ToList();
                    _userContext.labelsNotes.RemoveRange(labelsnotes);
                    await this._userContext.SaveChangesAsync();
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
                List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.NoteId == noteId).Join(_userContext.Labels,
                    labelnote => labelnote.LabelId,
                    label => label.Id,
                    (labelnote, label) => new LabelResponseModel
                    {
                        Id = labelnote.LabelId,
                        Label = label.Label,
                        IsCreated = label.IsCreated,
                        IsModified = label.IsModified
                    }).ToList();
                //// get list of collaborators
                List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                     .Where(note => note.NoteId == noteId)
                     .Join(_userContext.Users,
                     collab => collab.UserId,
                     user => user.Id,
                     (collab, user) => new CollaborationResponseModel
                     {
                         UserId = user.Id,
                         Email = user.Email,
                         Firstname = user.FirstName,
                         LastName = user.LastName
                     }).ToList();
                NoteResponseModel noteResponse = _userContext.Notes.Where(c => (c.NotesID == noteId) && (c.ID == userId)).Select(c => new NoteResponseModel
                {
                    Id = c.NotesID,
                    Title = c.Title,
                    Description = c.Description,
                    Reminder = c.Reminder,
                    Image = c.Image,
                    IsArchive = c.IsArchive,
                    IsTrash = c.IsTrash,
                    IsPin = c.IsPin,
                    IsCreated = c.IsCreated,
                    IsModified = c.IsModified,
                    labels = labelResponses,
                    collaborations =collaborationResponses
                    
                }).FirstOrDefault();
                return noteResponse;
            }
           
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> IsTrash(int UserId, int noteId, TrashArchivePin trash)
        {
            try
            {
                bool flag = trash.value;
                NotesModel notes = _userContext.Notes.FirstOrDefault(linq => (linq.NotesID == noteId) && (linq.ID == UserId));
                var collabNote = _userContext.collaborations.FirstOrDefault(linq => linq.NoteId == noteId);
                if (collabNote == null)
                {
                    if (notes != null)
                    {
                        if (flag)
                        {
                            notes.IsTrash = true;
                            var note = this._userContext.Notes.Attach(notes);
                            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            await this._userContext.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            notes.IsTrash = false;
                            var note = this._userContext.Notes.Attach(notes);
                            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            await this._userContext.SaveChangesAsync();
                            return true;
                        }
                    }
                }
                return false;
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
                        Id = linq.NotesID,
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
                if (notesModels.Count != 0 && notesModels != null)
                {
                    foreach (NoteResponseModel noteResponse in notesModels)
                    {
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.NoteId == noteResponse.Id).Join(_userContext.Labels,
                            labelnote => labelnote.LabelId,
                            label => label.Id,
                            (labelnote, label) => new LabelResponseModel
                            {
                                Id = labelnote.LabelId,
                                Label = label.Label,
                                IsCreated = label.IsCreated,
                                IsModified = label.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;
                    }
                }

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
        public async Task<bool> IsPin(int userId, int noteId, TrashArchivePin pin)
        {
            try
            {
                bool flag = pin.value;
               
                NotesModel notesModel = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == noteId));
                if (notesModel != null)
                {
                    if (flag)
                    {
                        notesModel.IsPin = true;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        return true;
                    }

                    else
                    {
                        notesModel.IsPin = false;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> IsArchive(int userId, int noteId, TrashArchivePin archive)
        {
            try
            {
                bool flag = archive.value;
                NotesModel notesModel = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == noteId));
                if (notesModel != null)
                {
                    if (flag)
                    {
                        notesModel.IsArchive = true;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        return true;

                    }
                    else
                    {
                        notesModel.IsArchive = false;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        await _userContext.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
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

                //// get list of labels
                if (notesModels.Count != 0 && notesModels != null)
                {
                    foreach (NoteResponseModel noteResponse in notesModels)
                    {
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.NoteId == noteResponse.Id).
                            Join(_userContext.Labels,
                            labelnote => labelnote.LabelId,
                            label => label.Id,
                            (labelnote, label) => new LabelResponseModel
                            {
                                Id = labelnote.LabelId,
                                Label = label.Label,
                                IsCreated = label.IsCreated,
                                IsModified = label.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;

                        //// get list of collaborators
                        List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                             .Where(note => note.NoteId == noteResponse.Id)
                             .Join(_userContext.Users,
                             collab => collab.UserId,
                             user => user.Id,
                             (collab, user) => new CollaborationResponseModel
                             {
                                 UserId = user.Id,
                                 Email = user.Email,
                                 Firstname = user.FirstName,
                                 LastName = user.LastName
                             }).ToList();
                        noteResponse.collaborations = collaborationResponses;
                    }
                }

              
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
                    Id = linq.NotesID,
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
                if (notesModels.Count != 0 && notesModels != null)
                {
                    foreach (NoteResponseModel noteResponse in notesModels)
                    {
                        //// list of labels
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.NoteId == noteResponse.Id).Join(_userContext.Labels,
                            labelnote => labelnote.LabelId,
                            label => label.Id,
                            (labelnote, label) => new LabelResponseModel
                            {
                                Id = labelnote.LabelId,
                                Label = label.Label,
                                IsCreated = label.IsCreated,
                                IsModified = label.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;

                        //// get list of collaborators
                        List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                             .Where(note => note.NoteId == noteResponse.Id)
                             .Join(_userContext.Users,
                             collab => collab.UserId,
                             user => user.Id,
                             (collab, user) => new CollaborationResponseModel
                             {
                                 UserId = user.Id,
                                 Email = user.Email,
                                 Firstname = user.FirstName,
                                 LastName = user.LastName
                             }).ToList();
                        noteResponse.collaborations = collaborationResponses;
                    }
                }
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
                List<NoteResponseModel> noteResponses = _userContext.labelsNotes.Where(linq => linq.Id == labelId).
                    Join(_userContext.Notes,
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
                if (noteResponses.Count != 0 && noteResponses != null)
                {
                    foreach (NoteResponseModel noteResponse in noteResponses)
                    {
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.
                            Where(note => note.NoteId == noteResponse.Id).Join(_userContext.Labels,
                            labelnote => labelnote.LabelId,
                            label => label.Id,
                            (labelnote, label) => new LabelResponseModel
                            {
                                Id = labelnote.LabelId,
                                Label = label.Label,
                                IsCreated = label.IsCreated,
                                IsModified = label.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;
                        List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                            .Where(note => note.NoteId == noteResponse.Id)
                            .Join(_userContext.Users,
                            collab => collab.UserId,
                            user => user.Id,
                            (collab, user) => new CollaborationResponseModel
                            {
                                UserId = user.Id,
                                Email = user.Email,
                                Firstname = user.FirstName,
                                LastName = user.LastName
                            }).ToList();
                        noteResponse.collaborations = collaborationResponses;
                    }
                }
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

       public NoteResponseModel ColourRequest(int noteId, ColourRequest colour, int userId)
        {
            var notesModel = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == noteId));
            if (notesModel != null)
            {
                notesModel.Color = colour.color;
                var user = this._userContext.Notes.Attach(notesModel);
                user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _userContext.SaveChanges();
            }
            NoteResponseModel noteResponse = _userContext.Notes.Where(c => (c.NotesID == noteId) && (c.ID == userId)).
                Select(c => new NoteResponseModel
            {
                Color = c.Color,
               
            }).FirstOrDefault();
            return noteResponse;
        }
       public List<NoteResponseModel> ReminderList(int userId)
        {
            try
            {
                List<NoteResponseModel> noteResponses = _userContext.Notes.Where(linq => linq.Reminder != null && linq.ID == userId && linq.IsTrash == false).
                    Select(linq => new NoteResponseModel
                    {
                        Id = linq.ID,
                        Title = linq.Title,
                        Description = linq.Description,
                        Reminder = linq.Reminder,
                        Image = linq.Image,
                        IsArchive = linq.IsArchive,
                        IsPin = linq.IsPin,
                        IsCreated = linq.IsCreated,
                        IsModified = linq.IsModified
                    }).ToList();
                if (noteResponses.Count != 0 && noteResponses != null)
                {
                    foreach (NoteResponseModel noteResponse in noteResponses)
                    {
                        List<LabelResponseModel> labelResponses = _userContext.labelsNotes.Where(note => note.NoteId == noteResponse.Id).Join(_userContext.Labels,
                            labelnote => labelnote.LabelId,
                            label => label.Id,
                            (labelnote, label) => new LabelResponseModel
                            {
                                Id = labelnote.LabelId,
                                Label = label.Label,
                                IsCreated = label.IsCreated,
                                IsModified = label.IsModified
                            }).ToList();
                        noteResponse.labels = labelResponses;
                        List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                            .Where(note => note.NoteId == noteResponse.Id)
                            .Join(_userContext.Users,
                            collab => collab.UserId,
                            user => user.Id,
                            (collab, user) => new CollaborationResponseModel
                            {
                                UserId = user.Id,
                                Email = user.Email,
                                Firstname = user.FirstName,
                                LastName = user.LastName
                            }).ToList();
                        noteResponse.collaborations = collaborationResponses;
                    }
                }
                noteResponses.Sort((note1, note2) => DateTime.Now.CompareTo(note1.Reminder.Value));
               

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
        public string UploadImage(int userId, int noteId, ImageUploadRequestModel image)
        {
            try
            {
                var data = _userContext.Notes.FirstOrDefault(linq => linq.ID == userId && linq.NotesID == noteId);
                if (data != null)
                {
                    string imageUrl = ImageUploadCloudinary.AddPhoto(image.ImageUrl);
                    data.Image = imageUrl;
                    var note = this._userContext.Notes.Attach(data);
                    note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this._userContext.SaveChanges();
                    return imageUrl;
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
        public NoteResponseModel Collaborations(int noteId, CollaborateMultiple collaboratorRequest)
        {
            try
            {
                var notesModel = _userContext.Notes.FirstOrDefault(linq => linq.NotesID == noteId);
                if (notesModel != null && collaboratorRequest.CollaboratorRequestModels.Count != 0)
                {

                    foreach (CollaboratorRequestModel requestModel in collaboratorRequest.CollaboratorRequestModels)
                    {
                        UserDB user = _userContext.Users.FirstOrDefault(linq => linq.Id == requestModel.UserId);
                        if (requestModel.UserId != 0 && user != null)
                        {
                            var data = new CollaborationModel()
                            {
                                NoteId = notesModel.NotesID,
                                UserId = user.Id
                            };
                            _userContext.collaborations.Add(data);
                            _userContext.SaveChangesAsync();
                        }
                    }
                }

                List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                      .Where(note => note.NoteId == notesModel.NotesID)
                      .Join(_userContext.Users,
                      collab => collab.UserId,
                      user => user.Id,
                      (collab, user) => new CollaborationResponseModel
                      {
                          UserId = user.Id,
                          Email = user.Email,
                          Firstname = user.FirstName,
                          LastName = user.LastName
                      }).ToList();
                NoteResponseModel noteResponse = new NoteResponseModel()
                {
                    Id = notesModel.ID,
                    Title = notesModel.Title,
                    Description = notesModel.Description,
                    Reminder = notesModel.Reminder,
                    IsCreated = notesModel.IsCreated,
                    IsModified = notesModel.IsModified,
                    IsPin = notesModel.IsPin,
                    IsArchive = notesModel.IsArchive,
                    Color = notesModel.Color,
                    Image = notesModel.Image,
                    collaborations = collaborationResponses
                };
                return noteResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private List<NoteResponseModel> SearchNote(int userId, string keyword)
        {
            List<NoteResponseModel> searchNoteResponses = null;
            if (keyword != null)
            {
                searchNoteResponses = _userContext.Notes.
                Where(linq => (linq.Title.Contains(keyword) || linq.Description.Contains(keyword)) && (linq.ID == userId)).
                Select(linq => new NoteResponseModel
                {
                    Id = linq.NotesID,
                    Title = linq.Title,
                    Description = linq.Description,
                    Reminder = linq.Reminder,
                    Image = linq.Image,
                    IsArchive = linq.IsArchive,
                    IsTrash = linq.IsTrash,
                    IsPin = linq.IsPin,
                    IsCreated = linq.IsCreated,
                    IsModified = linq.IsModified

                }).ToList();
                foreach (NoteResponseModel notecollab in searchNoteResponses)
                {
                    List<CollaborationResponseModel> collaborationResponses = _userContext.collaborations
                          .Where(note => note.NoteId == notecollab.Id)
                          .Join(_userContext.Users,
                          collab => collab.UserId,
                          user => user.Id,
                          (collab, user) => new CollaborationResponseModel
                          {
                              UserId = user.Id,
                              Email = user.Email,
                              Firstname = user.FirstName,
                              LastName = user.LastName
                          }).ToList();
                    notecollab.collaborations = collaborationResponses;
                }
            }
            return searchNoteResponses;
        }
    }
}
