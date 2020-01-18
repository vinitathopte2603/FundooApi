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

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private IConfiguration _config;
        public AccountsController(IUserBL userBL, IConfiguration configuration)
        {
            this._userBL = userBL;
            _config = configuration;
        }
        [HttpPost]
        [Route("registration")]
        public IActionResult Registration([FromBody]UserDB userDB)
        {
            var result = _userBL.Registration(userDB);
            if (result != null)
            {
                return this.Ok(new { result = "successfully added" });
            }
            else
            {
                return this.BadRequest(new { result = "failed to add" });
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]Login login)
        {
            var result = _userBL.Login(login);
            if (result != null)
            {
                var status = true;
                var message = "Login successful";
                var token = GenerateJSONWebToken(result, "Login");
                var token1 = token;
                var data = result;
                return this.Ok(new { status, message, data, token1 });
            }
            else
            {
                var status = false;
                var message = "Login failed";
                return this.BadRequest(new { status, message });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var result = _userBL.ForgotPassword(forgotPassword);
            if (result != null)
            {
                var status = true;
                var message = "Email verified";
                var token = GenerateJSONWebToken(result, "ForgotPassword");
                MsmqSend.MsmqSendMethod(token);
                string receivedToken = Receiver.ReceiveFromMsmq();
               string mailStatus = SendEmail.SendMail(receivedToken, forgotPassword);
                return this.Ok(new { status, message, mailStatus ,token});
            }
            else
            {
                var status = true;
                var message = "Email not verified ";
                return this.BadRequest(new { status, message });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassword resetPassword)
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
                        resetPassword.Id = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        status = _userBL.ResetPassword(resetPassword);
                        if (status)
                        {
                            status = true;
                            message = "Password successfully changed";
                            return Ok(new { status, message });
                        }
                    }
                }
                status = false;
                message = "Invalid Token";
                return NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        private string GenerateJSONWebToken(ResponseModel response, string type)
        {
            var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
        new Claim("Id", response.Id.ToString()),
        new Claim("Email", response.Email),
        new Claim("TokenType", type)
    };

            var token =new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                 claims,
        expires: DateTime.Now.AddMinutes(120),  
        signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}