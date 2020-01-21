//-----------------------------------------------------------------------
// <copyright file="INotesRepository.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Interfaces
{
    using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// method declaration
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>returns the added note</returns>
        NotesModel AddNotes(NotesModel notesModel);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the notes of specified ID</returns>
        List<NotesModel> GetAllNotes(int userId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>returns the updated notes</returns>
        NotesModel UpdateNotes(NotesModel notesModel);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a true if note is deleted else returns false</returns>
        bool DeleteNote(int userId, int notesId);

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        NotesModel GetNote(int userId, int noteId);

    }
}
