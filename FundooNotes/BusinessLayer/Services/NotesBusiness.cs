using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private INotesRepository _notesRepository;
        public NotesBusiness(INotesRepository notesRepository)
        {
            this._notesRepository = notesRepository;
        }
        public async Task<NoteResponseModel> AddNotes(NotesRequestModel notesModel, int userId)
        {
            try
            {
                if (notesModel != null && userId != 0)
                {
                    return await this._notesRepository.AddNotes(notesModel, userId);
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
            if (userId != 0)
            {
                return await this._notesRepository.DeleteAllTrash(userId);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteNote(int userId, int notesId)
        {
            if (userId != 0 && notesId != 0)
            {

                return await _notesRepository.DeleteNote(userId, notesId);
            }
            else
            {
                return false;
            }
        }

        public List<NoteResponseModel> GetAllArchive(int userId)
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

        public List<NoteResponseModel> GetAllNotes(int userId, string keyword)
        {
            if (userId != 0)
            {
                return _notesRepository.GetAllNotes(userId, keyword);
            }
            else
            {
                return null;
            }
        }

        public List<NoteResponseModel> GetAllPin(int userId)
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

        public List<NoteResponseModel> GetAllTrash(int userId)
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

        public NoteResponseModel GetNote(int userId, int noteId)
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

        public async Task<bool> IsArchive(int userId, int noteId, TrashArchivePin archive)
        {
            if (userId != 0 && noteId != 0)
            {
                return await _notesRepository.IsArchive(userId, noteId, archive);
            }
            else
            {
                return false; 
            }
        }

        public async Task<bool> IsPin(int userId, int noteId, TrashArchivePin pin)
        {
            if (userId != 0 && noteId != 0)
            {
                return await _notesRepository.IsPin(userId, noteId, pin);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsTrash(int userId, int noteId, TrashArchivePin trash)
        {
            if (userId != 0 && noteId != 0)
            {
                return await _notesRepository.IsTrash(userId, noteId, trash);
            }
            else
            {
                return false;
            }
        }

        public async Task<NoteResponseModel> UpdateNotes(NotesRequestModel notesModel,int noteId, int userId)
        {
            if (notesModel != null)
            {
                return await _notesRepository.UpdateNotes(notesModel, noteId, userId);
            }
            else
            {
                return null;
            }
        }
        public List<NoteResponseModel> GetNoteByLabelId(string label,int userId)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                return this._notesRepository.GetNoteByLabelId(label, userId);
            }
            else
            {
                return null;
            }
        }

        public NoteResponseModel ColourRequest(int noteId, ColourRequest colour, int userId)
        {
            if (noteId != 0 && colour != null && userId != 0)
            {
                return this._notesRepository.ColourRequest(noteId, colour, userId);
            }
            else
            {
                return null;
            }
        }
       public List<NoteResponseModel> ReminderList(int userId)
        {
            if (userId != 0)
            {
                return this._notesRepository.ReminderList(userId);
            }
            else
            {
                return null;
            }
        }

        public string UploadImage(int userId, int noteId, ImageUploadRequestModel image)
        {
            if (userId != 0 && noteId != 0 && image.ImageUrl != null)
            {
                return this._notesRepository.UploadImage(userId, noteId, image);
            }
            else 
            {
                return null;
            }
        }

        public NoteResponseModel Collaborations(int noteId, CollaborateMultiple collaboratorRequest, int ownerId)
        {
            if (noteId != 0 && collaboratorRequest.CollaboratorRequestModels.Count != 0)
            {
                return this._notesRepository.Collaborations(noteId, collaboratorRequest,ownerId);
            }
            else
            {
                return null;
            }
        }

        public List <GetUsersResponseModel> GetAllUsers(string keyword)
        {
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                return _notesRepository.GetAllUsers(keyword);
            }
            else
            {
                return null;
            }
        }
    }
}
