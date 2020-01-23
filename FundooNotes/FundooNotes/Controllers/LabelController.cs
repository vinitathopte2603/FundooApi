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
    public class LabelController : ControllerBase
    {
        private ILabelsBusiness _labelsBusiness;
        public LabelController(ILabelsBusiness labelsBusiness)
        {
            this._labelsBusiness = labelsBusiness;
        }
        [HttpPost]
        [Route("Addlabel")]
        public IActionResult AddLabel([FromBody]string label)
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
                        var data = this._labelsBusiness.AddLabel(label, userId);
                        if (data != null)
                        {
                            status = true;
                            message = "label created";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }

                status = false;
                message = "label not created";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        [HttpGet]
        [Route("GetLabels")]
        public IActionResult GetAllLabels()
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
                        var data = this._labelsBusiness.GetAllLabels(userId);
                        if (data != null)
                        {
                            status = true;
                            message = "All Available labels";
                            return this.Ok(new { status, message, data });
                        }
                    }
                }
                status = false;
                message = "label not available";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }
        [HttpDelete]
        [Route("Deletelabel")]
        public IActionResult DeleteLabel(int labelId)
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
                        var result = this._labelsBusiness.DeleteLabel(userId, labelId);
                        if (result)
                        {
                            status = true;
                            message = "label deleted";
                            return this.Ok(new { status, message });

                        }
                    }
                }
                status = false;
                message = "not found";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }
        [HttpPut]
        [Route("updatelabel")]
        public IActionResult UpdateLabel(int labelId, string label)
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
                        var data = this._labelsBusiness.UpdateLabel(userId, labelId, label);
                        if (data != null)
                        {
                            status = true;
                            message = "label deleted";
                            return this.Ok(new { status, message, data });

                        }
                    }
                }
                status = false;
                message = "not found";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.NotFound(new { e.Message });
            }
        }
    }
}