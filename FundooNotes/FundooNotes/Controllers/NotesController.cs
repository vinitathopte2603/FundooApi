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
        public IActionResult AddNotes([FromBody] NotesRequestModel notesModel)
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
                        var data = this._notesBusiness.AddNotes(notesModel, userId);
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
        public async Task<IActionResult> UpdateNote([FromBody] NotesRequestModel notesModel1,int noteId)
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
                        if (result!=null)
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
        public IActionResult GetAllNotes()
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
                        List<NoteResponseModel> result = this._notesBusiness.GetAllNotes(userId);
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
                        List<NoteResponseModel> result = this._notesBusiness.GetAllTrash(userId);
                        if (result != null)
                        {
                            status = true;
                            message = "note";
                            return this.Ok(new { status, message, result });
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
                        List<NoteResponseModel> result = this._notesBusiness.GetAllArchive(userId);
                        if (result != null)
                        {
                            status = true;
                            message = "Archived notes";
                            return this.Ok(new { status, message, result });
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
        public async Task<IActionResult> IsPin(int noteId)
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
                        bool result = await this._notesBusiness.IsPin(userId, noteId);
                        if (result)
                        {
                            status = true;
                            message = "note pinned";
                            return this.Ok(new { status, message });
                        }
                    }
                }
                status = true;
                message = "note  unpinned";
                return this.Ok(new { status, message });
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
        public async Task<IActionResult> IsArchive(int noteId)
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
                        bool result = await this._notesBusiness.IsArchive(userId, noteId);
                        if (result)
                        {
                            status = true;
                            message = "note archived";
                            return this.Ok(new { status, message });
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
        public async Task<IActionResult> IsTrash(int noteId)
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
                        bool result = await this._notesBusiness.IsTrash(userId, noteId);
                        if (result)
                        {
                            status = true;
                            message = "note archived";
                            return this.Ok(new { status, message });
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
        [Route("labelId")]
        public IActionResult GetNoteByLabelId(int labelId)
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
                        List<NoteResponseModel> result = this._notesBusiness.GetNoteByLabelId(labelId);
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
      
    }
}