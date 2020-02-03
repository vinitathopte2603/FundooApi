using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.UserRequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminSignUpBusiness _adminSignUpBusiness;
        private IConfiguration _config;
        public AdminController(IAdminSignUpBusiness adminSignUpBusiness, IConfiguration configuration)
        {
            this._adminSignUpBusiness = adminSignUpBusiness;
            this._config = configuration;

        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> AdminRegistration([FromBody] RegistrationRequestModel registrationRequest)
        {
            try
            {
                var data = await this._adminSignUpBusiness.AdminRegistration(registrationRequest);
                bool status;
                string message;
                if (data != null)
                {
                    status = true;
                    message = "registration successful";
                    return this.Ok(new { status, message, data });
                }
                status = false;
                message = "registration failed";
                return this.BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]Login login)
        {
            try
            {
                var result = this._adminSignUpBusiness.AdminLogin(login);
                if (result != null)
                {
                    var status = true;
                    var message = "Login successful";
                    var token = GenerateJSONWebToken(result, "Login");
                    var data = result;
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
        [HttpGet]
        [Route("statistics")]
        public IActionResult Statistics()
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
                        if (user.Claims.FirstOrDefault(c => c.Type == "UserType").Value == "Admin")
                        {
                            int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                            var data = _adminSignUpBusiness.Statistics(userId);
                            if (data != null)
                            {
                                status = true;
                                message = "statistics";
                                return this.Ok(new { status, message, data });
                            }
                        }
                    }
                }
                status = false;
                message = "data not found";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers(int pageNumber, int pageSize)
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
                        if (user.Claims.FirstOrDefault(c => c.Type == "UserType").Value == "Admin")
                        {
                            int userId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                            var data = _adminSignUpBusiness.GetUsers(pageNumber, pageSize);
                            if (data != null)
                            {
                                status = true;
                                message = "users";
                                return this.Ok(new { status, message, data });
                            }
                        }
                    }
                }
                status = false;
                message = "data not found";
                return this.NotFound(new { status, message });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
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
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}