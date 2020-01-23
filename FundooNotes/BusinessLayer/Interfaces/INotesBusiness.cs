using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Interfaces
{
    public interface INotesBusiness
    {
        NotesModel AddNotes(NotesModel notesModel);
        NotesModel UpdateNotes(NotesModel notesModel);
        bool DeleteNote(int userId, int noteId);
        NotesModel GetNote(int userId, int noteId);
        List<NotesModel> GetAllNotes(int userId);
        bool IsPin(int userId, int noteId);
        bool IsArchive(int userId, int noteId);
        bool IsTrash(int userId, int noteId);
        List<NotesModel> GetAllTrash(int userId);
        List<NotesModel> GetAllPin(int userId);
        List<NotesModel> GetAllArchive(int userId);
    }
}
