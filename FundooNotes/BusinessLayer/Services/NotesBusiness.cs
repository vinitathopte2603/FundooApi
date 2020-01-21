using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private INotesRepository _notesRepository;
        public NotesBusiness(INotesRepository notesRepository)
        {
            this._notesRepository = notesRepository;
        }
        public NotesModel AddNotes(NotesModel notesModel)
        {
            try
            {
                if (notesModel != null)
                {
                    return this._notesRepository.AddNotes(notesModel);
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

        public bool DeleteNote(int userId, int notesId)
        {
            if (userId != 0 && notesId != 0)
            {

                return _notesRepository.DeleteNote(userId, notesId);
            }
            else
            {
                return false;
            }
        }

        public List<NotesModel> GetAllNotes(int userId)
        {
            if (userId != 0)
            {
                return _notesRepository.GetAllNotes(userId);
            }
            else
            {
                return null;
            }
        }

        public NotesModel GetNote(int userId, int noteId)
        {
            if (userId != 0 && noteId != 0)
            {

                return _notesRepository.GetNote(userId, noteId);
            }
            else
            {
                return null;
            }
        }

        public NotesModel UpdateNotes(NotesModel notesModel)
        {
            if (notesModel != null)
            {
                return _notesRepository.UpdateNotes(notesModel);
            }
            else
            {
                return null;
            }
        }
    }
}
