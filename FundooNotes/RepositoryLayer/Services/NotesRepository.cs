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
            List<NotesModel> notes = _userContext.Notes.Where(linq => linq.ID == userId).ToList();
            if (notes != null)
            {
                return notes;
            }
            else
            {
                return null;
            }
        }

        public NotesModel UpdateNotes(NotesModel notesModel)
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
        public bool DeleteNote(int userId,int notesId)
        {
            NotesModel note = _userContext.Notes.FirstOrDefault(linq => (linq.ID == userId) && (linq.NotesID == notesId));
            if (note != null)
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

        public NotesModel GetNote(int userId, int noteId)
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
    }
}
