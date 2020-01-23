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

        public List<NotesModel> GetAllArchive(int userId)
        {
            if (userId != 0)
            {
                return this._notesRepository.GetAllArchive(userId);
            }
            else
            {
                return null;
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

        public List<NotesModel> GetAllPin(int userId)
        {
            if (userId != 0)
            {
                return this._notesRepository.GetAllPin(userId);
            }
            else
            {
                return null;
            }
        }

        public List<NotesModel> GetAllTrash(int userId)
        {
            if (userId != 0)
            {
                return _notesRepository.GetAllTrash(userId);
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

        public bool IsArchive(int userId, int noteId)
        {
            if (userId != 0 && noteId != 0)
            {
                return _notesRepository.IsArchive(userId, noteId);
            }
            else
            {
                return false; 
            }
        }

        public bool IsPin(int userId, int noteId)
        {
            if (userId != 0 && noteId != 0)
            {
                return _notesRepository.IsPin(userId, noteId);
            }
            else
            {
                return false;
            }
        }

        public bool IsTrash(int userId, int noteId)
        {
            if (userId != 0 && noteId != 0)
            {
                return _notesRepository.IsTrash(userId, noteId);
            }
            else
            {
                return false;
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
