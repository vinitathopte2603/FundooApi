//-----------------------------------------------------------------------
// <copyright file="INotesRepository.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Interfaces
{
    using System;
using System.Collections.Generic;
using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.UserRequestModel;

    /// <summary>
    /// method declaration
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notesModel.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the list of notes</returns>
        Task<NoteResponseModel> AddNotes(NotesRequestModel notesModel, int userId);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns>returns list of notes</returns>
        List<NoteResponseModel> GetAllNotes(int userId, string keyword);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the list of notes</returns>
        Task<NoteResponseModel> UpdateNotes(NotesRequestModel notesModel, int noteId, int userId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a true if note is deleted else returns false</returns>
        Task<bool> DeleteNote(int userId, int notesId);

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a note</returns>
        NoteResponseModel GetNote(int userId, int noteId);

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> IsPin(int userId, int noteId, TrashArchivePin pin);

        /// <summary>
        /// Determines whether the specified user identifier is archive.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="archive">The archive.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> IsArchive(int userId, int noteId, TrashArchivePin archive);

        /// <summary>
        /// Determines whether the specified user identifier is trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="trash">The trash.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> IsTrash(int userId, int noteId, TrashArchivePin trash);

        /// <summary>
        /// Gets all trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the notes in trash</returns>
        List<NoteResponseModel> GetAllTrash(int userId);

        /// <summary>
        /// Gets all pin.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the notes that are pinned</returns>
        List<NoteResponseModel> GetAllPin(int userId);

        /// <summary>
        /// Gets all archive.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns all archived notes</returns>
        List<NoteResponseModel> GetAllArchive(int userId);

        /// <summary>
        /// Deletes all trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> DeleteAllTrash(int userId);

        /// <summary>
        /// Gets the note by label identifier.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns list of notes</returns>
        List<NoteResponseModel> GetNoteByLabelId(string label, int userId);

        /// <summary>
        /// Colors the request.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="colour">The color.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns a note</returns>
        NoteResponseModel ColourRequest(int noteId, ColourRequest colour, int userId);

        /// <summary>
        /// Reminders the list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns a list of notes</returns>
        List<NoteResponseModel> ReminderList(int userId);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>the image url</returns>
        string UploadImage(int userId, int noteId, ImageUploadRequestModel image);

        /// <summary>
        /// Collaborates the specified note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="collaboratorRequest">The collaborator request.</param>
        /// <returns>the note</returns>
        NoteResponseModel Collaborations(int noteId, CollaborateMultiple collaboratorRequest, int ownerId);
    }
}
