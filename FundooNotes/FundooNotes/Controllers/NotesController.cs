using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INotesBusiness _notesBusiness;
        public NotesController(INotesBusiness notesBusiness)
        {
            this._notesBusiness = notesBusiness;
        }

        [HttpPost]
       // [Route("AddNotes")]
        public IActionResult AddNotes([FromBody] NotesModel notesModel)
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
                        notesModel.ID = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        notesModel = this._notesBusiness.AddNotes(notesModel);
                        if (notesModel != null)
                        {
                            status = true;
                            message = "Note created";
                            return this.Ok(new { status, message, notesModel });
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

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateNote([FromBody] NotesModel notesModel)
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
                        notesModel.ID = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        notesModel = this._notesBusiness.UpdateNotes(notesModel);
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
                return this.NotFound(new { e.Message });
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteNote(int noteId)
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
                        bool result = this._notesBusiness.DeleteNote(userId, noteId);
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
                return this.NotFound(new { e.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
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
                        NotesModel result = this._notesBusiness.GetNote(userId, noteId);
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
                return this.NotFound(new { e.Message });
            }
        }

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
                        List<NotesModel> result = this._notesBusiness.GetAllNotes(userId);
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
                return this.NotFound(new { e.Message });
            }
        }

        [HttpGet]
        [Route("GetAllTrash")]
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
                        List<NotesModel> result = this._notesBusiness.GetAllTrash(userId);
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
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }

        [HttpGet]
        [Route("GetAllPin")]
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
                        List<NotesModel> result = this._notesBusiness.GetAllPin(userId);
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
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }

        [HttpGet]
        [Route("GetAllArchive")]
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
                        List<NotesModel> result = this._notesBusiness.GetAllArchive(userId);
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
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }

        [HttpPut]
        [Route("IsPin")]
        public IActionResult IsPin(int noteId)
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
                        bool result = this._notesBusiness.IsPin(userId, noteId);
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
        [HttpPut]
        [Route("IsArchive")]
        public IActionResult IsArchive(int noteId)
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
                        bool result = this._notesBusiness.IsArchive(userId, noteId);
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
        [HttpPut]
        [Route("Istrash")]
        public IActionResult IsTrash(int noteId)
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
                        bool result = this._notesBusiness.IsTrash(userId, noteId);
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
      
    }
}