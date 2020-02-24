//-----------------------------------------------------------------------
// <copyright file="LabelController.cs" author="Vinita Thopte" company="Bridgelabz">
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
    /// label controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The labels business
        /// </summary>
        private ILabelsBusiness _labelsBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelsBusiness">The labels business.</param>
        public LabelController(ILabelsBusiness labelsBusiness)
        {
            this._labelsBusiness = labelsBusiness;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>returns specified action</returns>
        [HttpPost]
       // [Route("Addlabel")]
        public async Task<IActionResult> AddLabel([FromBody]LabelsRequestModel label)
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
                        var data = await this._labelsBusiness.AddLabel(label, userId);
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

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>returns the result of specified action</returns>
        [HttpGet]
       // [Route("GetLabels")]
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
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpDelete]
        [Route("{labelId}")]
        public async Task<IActionResult> DeleteLabel(int labelId)
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
                        var result = await this._labelsBusiness.DeleteLabel(userId, labelId);
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
                return this.BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>returns the result of specified action</returns>
        [HttpPut]
        [Route("{labelId}")]
        public async Task<IActionResult> UpdateLabel(int labelId, LabelsRequestModel label)
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
                        var data = await this._labelsBusiness.UpdateLabel(userId, labelId, label);
                        if (data != null)
                        {
                            status = true;
                            message = "label updated";
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
                return this.BadRequest(new { e.Message });
            }
        }
    }
}