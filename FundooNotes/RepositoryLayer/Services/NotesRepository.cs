using FundooCommonLayer.Model;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepositoryLayer.Services
{
    public class NotesRepository : INotesRepository
    {
        private UserContext _userContext;
        public NotesRepository(UserContext userContext)
        {
            this._userContext = userContext;
        }
        public NotesModel AddNotes(NotesModel notesModel)
        {
            try
            {
                notesModel.IsCreated = DateTime.Now;
                notesModel.IsModified = DateTime.Now;
                this._userContext.Notes.Add(notesModel);
                _userContext.SaveChanges();
                return notesModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NotesModel> GetAllNotes(int userId)
        {
            try
            {
                List<NotesModel> notes = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == false) && (linq.IsArchive == false)).ToList();
                if (notes.Count != 0)
                {
                    return notes;
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

        public NotesModel UpdateNotes(NotesModel notesModel)
        {
            try
            {
                NotesModel notes = _userContext.Notes.FirstOrDefault(linq => (linq.ID == notesModel.ID) && (linq.NotesID == notesModel.NotesID));
                if (notes != null)
                {
                    notes.Title = notesModel.Title;
                    notes.Description = notesModel.Description;
                    notes.IsModified = DateTime.Now;
                    var note = this._userContext.Notes.Attach(notes);
                    note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this._userContext.SaveChanges();
                    return notes;
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
        public bool DeleteNote(int userId,int notesId)
        {
            try
            {
                NotesModel note = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == notesId));
                if (note != null)
                {
                    if (note.IsTrash == true)
                    {
                        _userContext.Notes.Remove(note);
                        this._userContext.SaveChanges();
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

        public NotesModel GetNote(int userId, int noteId)
        {
            try
            {
                NotesModel notes = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == noteId));
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsTrash(int UserId, int noteId)
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
                        this._userContext.SaveChanges();
                        flag = true;
                    }
                    else
                    {
                        notes.IsTrash = false;
                        var note = this._userContext.Notes.Attach(notes);
                        note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        this._userContext.SaveChanges();
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

        public List<NotesModel> GetAllTrash(int userId)
        {
            try
            {
                List<NotesModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == true) && (linq.IsArchive == false) && (linq.IsPin == false)).ToList();
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
        public bool IsPin(int userId, int noteId)
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
                        _userContext.SaveChanges();
                        flag = true;

                    }
                    else
                    {
                        notesModel.IsPin = false;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        _userContext.SaveChanges();
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

        public bool IsArchive(int userId, int noteId)
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
                        _userContext.SaveChanges();
                        flag = true;

                    }
                    else
                    {
                        notesModel.IsArchive = false;
                        var user = this._userContext.Notes.Attach(notesModel);
                        user.State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                        _userContext.SaveChanges();
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

        public List<NotesModel> GetAllPin(int userId)
        {
            try
            {
                List<NotesModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == false) && (linq.IsArchive == false) && (linq.IsPin == true)).ToList();
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
        public List<NotesModel> GetAllArchive(int userId)
        {
            try
            {
                List<NotesModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == false) && (linq.IsArchive == true) && (linq.IsPin == false)).ToList();
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
        public bool DeleteAllTrash(int userId)
        {
            try
            {
                List<NotesModel> notesModels = _userContext.Notes.Where(linq => (linq.ID == userId) && (linq.IsTrash == true) && (linq.IsArchive == false) && (linq.IsPin == false)).ToList();
                if (notesModels.Count != 0)
                {
                    _userContext.Notes.RemoveRange(notesModels);
                    _userContext.SaveChanges();
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

    }
}
