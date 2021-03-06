﻿//-----------------------------------------------------------------------
// <copyright file="AccountsController.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FundooBusinessLayer.Interfaces;
using FundooBusinessLayer.Services;
using FundooCommonLayer.Model;
using FundooCommonLayer.MSMQ;
using FundooCommonLayer.UserRequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;


    /// <summary>
    /// controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// object declaration of mentioned interface
        /// </summary>
        private readonly IUserBL _userBL;

        /// <summary>
        /// object declaration of mentioned interface
        /// </summary>
        private IConfiguration _config;

        /// <summary>
        /// Initializes the new instance of the <see cref="AccountsController"/>class
        /// </summary>
        /// <param name="userBL">The userBL</param>
        /// <param name="configuration">The configuration</param>
        public AccountsController(IUserBL userBL, IConfiguration configuration)
        {
            this._userBL = userBL;
            this._config = configuration;
        }

        /// <summary>
        /// registration of a new user
        /// </summary>
        /// <param name="userDB">field of UserDB type</param>
        /// <returns>return the specified action</returns>
        [HttpPost]
        [Route("registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationRequestModel userDB)
        {
            try
            {
                var data =await  this._userBL.Registration(userDB);
                bool status;
                string message;
                if (data != null)
                {
                    if (IsFieldEmpty(userDB))
                    {
                        status = true;
                        message = "Successfully added";
                        return this.Ok(new { status, message, data });
                    }
                    else
                    {
                        status = false;
                        message = "field cannot be empty";
                        return this.BadRequest(new { status, message });
                    }
                }
                else
                {
                    status = false;
                    message = "failed to add";
                    return this.BadRequest(new { status, message });
                }
            }
                
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

 
        /// <summary>
        /// Login of the existing user
        /// </summary>
        /// <param name="login">field of Login type</param>
        /// <returns>returns user data if successful else returns null</returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]Login login)
        {
            try
            {
                var result = this._userBL.Login(login);
                if (result != null)
                {   
                    var status = true;
                    var message = "Login successful";
                    var token = this.GenerateJSONWebToken(result, "Login");
                    var data = result;
                    //ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost");
                    //IDatabase db = connection.GetDatabase();
                    //db.StringSet(result.Id.ToString(), token,TimeSpan.FromMinutes(1440));
                    return this.Ok(new { status, message, data, token });
                } 
                else
                {
                    var status = false;
                    var message = "Login failed";
                    return this.BadRequest(new { status, message });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// http post method for forget password
        /// </summary>
        /// <param name="forgotPassword">the email address</param>
        /// <returns>returns the specified status</returns>
        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            try
            {
                var result = this._userBL.ForgotPassword(forgotPassword);
                if (result != null)
                {
                    var status = true;
                    var message = "mail sent to registered email";
                    var token = this.GenerateJSONWebToken(result, "ForgotPassword");
                    MsmqSend.MsmqSendMethod(token,forgotPassword.Email);
                    return this.Ok(new { status, message });
                }
                else
                {
                    var status = true;
                    var message = "Email not verified ";
                    return this.NotFound(new { status, message });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to reset the password
        /// </summary>
        /// <param name="resetPassword">the new password</param>
        /// <returns>returns the specified action</returns>
        
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var user = HttpContext.User;
                bool status;
                string message;
                if (user.HasClaim(c => c.Type == "TokenType"))
                {
                    if (user.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "ForgotPassword")
                    {
                        int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        status = await this._userBL.ResetPassword(resetPassword, userId);
                        if (status)
                        {
                            status = true;
                            message = "Password successfully changed";
                            return this.Ok(new { status, message });
                        }
                    }
                }

                status = false;
                message = "Invalid Token";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { e.Message });
            }
        }

        [HttpPut]
        [Route("profilepicture")]
        public IActionResult ProfilePicture([FromForm] ImageUploadRequestModel image)
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
                        string imageUrl = this._userBL.ProfilePicture(userId, image);
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

        /// <summary>
        /// Token generation
        /// </summary>
        /// <param name="response">the field of type Response Model</param>
        /// <param name="type">the token type</param>
        /// <returns>returns the token</returns>
        private string GenerateJSONWebToken(ResponseModel response, string type)
        {
           
            try
            {
                var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:Key"]));
                var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim("Id", response.Id.ToString()),
                new Claim("Email", response.Email),
                new Claim("TokenType", type),
                new Claim("UserType", response.UserRole)
            };
                var token = new JwtSecurityToken(this._config["Jwt:Issuer"],
                    this._config["Jwt:Issuer"],
                     claims,
            expires: DateTime.Now.AddMinutes(1440),
            signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private bool IsFieldEmpty(RegistrationRequestModel registration)
        {
           
            if (string.IsNullOrWhiteSpace(registration.FirstName) || registration.FirstName.Length<3||registration.FirstName.Length>15
                || string.IsNullOrWhiteSpace(registration.LastName) || registration.LastName.Length < 3 || registration.LastName.Length > 15
                || !registration.Email.Contains(".") || !registration.Email.Contains("@")||
                string.IsNullOrWhiteSpace(registration.Email) || string.IsNullOrWhiteSpace(registration.Passwrod) || string.IsNullOrWhiteSpace(registration.Type)
                || registration.IsActive == null )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}