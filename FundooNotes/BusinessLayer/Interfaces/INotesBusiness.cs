//-----------------------------------------------------------------------
// <copyright file="INotesBusiness.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Interfaces
{
    using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

    /// <summary>
    /// method declaration
    /// </summary>
    public interface INotesBusiness
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the notes</returns>
        Task<NoteResponseModel> AddNotes(NotesRequestModel notesModel, int userId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns th eupdated notes</returns>
        Task<NoteResponseModel> UpdateNotes(NotesRequestModel notesModel, int noteId, int userId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the boolean value</returns>
        Task<bool> DeleteNote(int userId, int noteId);

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a single note</returns>
        NoteResponseModel GetNote(int userId, int noteId);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the list of notes</returns>
        List<NoteResponseModel> GetAllNotes(int userId);

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> IsPin(int userId, int noteId, TrashArchivePin pin);

        /// <summary>
        /// Determines whether the specified user identifier is archive.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> IsArchive(int userId, int noteId, TrashArchivePin archive);

        /// <summary>
        /// Determines whether the specified user identifier is trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> IsTrash(int userId, int noteId, TrashArchivePin trash);

        /// <summary>
        /// Gets all trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the list of notes in trash</returns>
        List<NoteResponseModel> GetAllTrash(int userId);

        /// <summary>
        /// Gets all pin.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the pinned notes</returns>
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
        /// <returns>returns a list notes</returns>
        List<NoteResponseModel> GetNoteByLabelId(int labelId);
        NoteResponseModel ColourRequest(int noteId, ColourRequest colour, int userId);
        List<NoteResponseModel> ReminderList(int userId);
        string UploadImage(int userId, int noteId, ImageUploadRequestModel image);
    }
}
