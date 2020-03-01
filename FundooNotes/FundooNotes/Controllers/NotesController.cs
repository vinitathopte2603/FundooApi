//-----------------------------------------------------------------------
// <copyright file="NotesController.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
    using StackExchange.Redis;


    /// <summary>
    /// Notes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes business
        /// </summary>
        private INotesBusiness _notesBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesBusiness">The notes business.</param>
        public NotesController(INotesBusiness notesBusiness)
        {
            this._notesBusiness = notesBusiness;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpPost]
        // [Route("AddNotes")]
        public async Task<IActionResult> AddNotes([FromBody] NotesRequestModel notesModel)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                NoteResponseModel model = new NoteResponseModel();
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        var data = await this._notesBusiness.AddNotes(notesModel, userId);
                        if (data != null)
                        {
                            status = true;
                            message = "Note created";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "Note not created";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="notesModel1">The notes model1.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpPut]
        [Route("{noteId}")]
        public async Task<IActionResult> UpdateNote([FromBody] NotesRequestModel notesModel1, int noteId)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        NoteResponseModel notesModel = await this._notesBusiness.UpdateNotes(notesModel1, noteId, userId);
                        if (notesModel != null)
                        {
                            status = true;
                            message = "Note updated";
                            return this.Ok(new { status, message, notesModel });
                        }
                    }
                }

                status = false;
                message = "Note not updated";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpDelete]
        [Route("{noteId}")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        bool result = await this._notesBusiness.DeleteNote(userId, noteId);
                        if (result)
                        {
                            status = true;
                            message = "Note deleted";
                            return this.Ok(new { status, message });
                        }
                    }
                }

                status = false;
                message = "Note not deleted";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
        [Route("{noteId}")]
        public IActionResult GetNote(int noteId)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        NoteResponseModel result = this._notesBusiness.GetNote(userId, noteId);
                        if (result != null)
                        {
                            status = true;
                            message = "note";
                            return this.Ok(new { status, message, result });
                        }
                    }
                }

                status = false;
                message = "Note not available";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
        //  [Route("GetAllNotes")]
        public IActionResult GetAllNotes(string keyword)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        List<NoteResponseModel> data = this._notesBusiness.GetAllNotes(userId, keyword);
                        if (data != null)
                        {
                            status = true;
                            message = "note";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "Note not available";
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets all trash.
        /// </summary>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
        [Route("AllTrash")]
        public IActionResult GetAllTrash()
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        List<NoteResponseModel> data = this._notesBusiness.GetAllTrash(userId);
                        if (data != null)
                        {
                            status = true;
                            message = "note";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "trash is empty";
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets all pin.
        /// </summary>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
        [Route("AllPin")]
        public IActionResult GetAllPin()
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        List<NoteResponseModel> result = this._notesBusiness.GetAllPin(userId);
                        if (result != null)
                        {
                            status = true;
                            message = "pinned notes";
                            return this.Ok(new { status, message, result });
                        }
                    }
                }

                status = false;
                message = "Note not available";
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets all archive.
        /// </summary>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
        [Route("AllArchive")]
        public IActionResult GetAllArchive()
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        List<NoteResponseModel> data = this._notesBusiness.GetAllArchive(userId);
                        if (data != null)
                        {
                            status = true;
                            message = "Archived notes";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "Archive is empty";
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is pin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpPut]
        [Route("{noteId}/Pinned")]
        public async Task<IActionResult> IsPin(int noteId, [FromBody] TrashArchivePin pin)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        bool result = await this._notesBusiness.IsPin(userId, noteId, pin);
                        if (result == true && pin.value == true)
                        {
                            status = true;
                            message = "note pinned";
                            return this.Ok(new { status, message });
                        }
                        if (result == true && pin.value == false)
                        {
                            status = true;
                            message = "note unpinned";
                            return this.Ok(new { status, message });
                        }
                        if (!result)
                        {
                            status = false;
                            message = "note not found ";
                            return this.NotFound(new { status, message });
                        }
                    }
                }
                status = false;
                message = "Note not available";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpPut]
        [Route("{noteId}/Archived")]
        public async Task<IActionResult> IsArchive(int noteId, [FromBody] TrashArchivePin archive)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        bool result = await this._notesBusiness.IsArchive(userId, noteId, archive);
                        if (result == true && archive.value == true)
                        {
                            status = true;
                            message = "note archived";
                            return this.Ok(new { status, message });
                        }
                        if (result == true && archive.value == false)
                        {
                            status = true;
                            message = "note unarchived";
                            return this.Ok(new { status, message });
                        }
                        if (!result)
                        {
                            status = false;
                            message = "note not found ";
                            return this.NotFound(new { status, message });
                        }
                    }
                }

                status = false;
                message = "Note not available";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpPut]
        [Route("{noteId}/trash")]
        public async Task<IActionResult> IsTrash(int noteId, TrashArchivePin trash)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        bool result = await this._notesBusiness.IsTrash(userId, noteId, trash);
                        if (result == true && trash.value == true)
                        {
                            status = true;
                            message = "note moved to trash";
                            return this.Ok(new { status, message });
                        }
                        if (result == true && trash.value == false)
                        {
                            status = true;
                            message = "note moved from trash";
                            return this.Ok(new { status, message });
                        }
                        if (!result)
                        {
                            status = false;
                            message = "note not found ";
                            return this.NotFound(new { status, message });
                        }
                    }
                }

                status = false;
                message = "Note not available";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Deletes all trash.
        /// </summary>
        /// <returns>returns the result of specified action</returns>
        [HttpDelete]
        //[Route("EmptyTrash")]
        public async Task<IActionResult> DeleteAllTrash()
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        bool result = await this._notesBusiness.DeleteAllTrash(userId);
                        if (result)
                        {
                            status = true;
                            message = "Notes deleted";
                            return this.Ok(new { status, message });
                        }
                    }
                }

                status = false;
                message = "Trash Empty";
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets the note by label identifier.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
        [Route("{label}/notebylabel")]
        public IActionResult GetNoteByLabelId(string label)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        List<NoteResponseModel> data = this._notesBusiness.GetNoteByLabelId(label,userId);
                        if (data != null)
                        {
                            status = true;
                            message = "note";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "Note not available";
                return this.Ok(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        [HttpPut]
        [Route("{noteId}/color")]
        public IActionResult ColourRequest(int noteId, [FromBody] ColourRequest colour)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        NoteResponseModel result = this._notesBusiness.ColourRequest(noteId, colour, userId);
                        if (result != null)
                        {
                            status = true;
                            message = "colour added";
                            return this.Ok(new { status, message });
                        }
                    }
                }
                status = false;
                message = "note not found";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("reminder")]
        public IActionResult ReminderList()
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                       List<NoteResponseModel> data =  this._notesBusiness.ReminderList(userId);
                        if (data.Count!=0)
                        {
                            status = true;
                            message = "reminder notes";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "list Empty";
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }
        [HttpPut]
        [Route("{noteId}/Imageupload")]
        public IActionResult UploadImage(int noteId,[FromForm] ImageUploadRequestModel image)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        string imageUrl = this._notesBusiness.UploadImage(userId, noteId, image);
                        if (imageUrl != null)
                        {
                            status = true;
                            message = "Image uploaded successfully";
                            return this.Ok(new { status, message, imageUrl });
                        }
                    }
                }
                status = false;
                message = "Image upload failed";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("{noteId}/collaborate")]
        public IActionResult AddCollaborator(int noteId, CollaborateMultiple collaborator)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        int ownerId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        NoteResponseModel data = this._notesBusiness.Collaborations(noteId, collaborator, ownerId);
                        if (data != null)
                        {
                            status = true;
                            message = "collaborated successfully";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }
                status = false;
                message = "collaboration failed";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}