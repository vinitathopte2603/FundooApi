//-----------------------------------------------------------------------
// <copyright file="UserRL.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Services
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.UserRequestModel;
    using FundooRepositoryLayer.Interfaces;
    using FundooRepositoryLayer.ModelContext;

    /// <summary>
    /// Implementation of the User interface of repository layer
    /// </summary>
    /// <seealso cref="FundooRepositoryLayer.Interfaces.IUserRL" />
    public class UserRL : IUserRL
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly UserContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRL"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRL(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// checks if the given email is present in database in order to reset the forgotten password
        /// </summary>
        /// <param name="forgotPassword">email address.</param>
        /// <returns>
        /// returns the user data to verify before resetting new password
        /// </returns>
        public ResponseModel ForgotPassword(ForgotPassword forgotPassword)
        {
            try
            {
                var data = this._context.Users.FirstOrDefault(user => user.Email == forgotPassword.Email);
                if (data != null)
                {
                    var userdata = new ResponseModel()
                    {
                        Id = data.Id,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Email = data.Email,
                        IsActive = data.IsActive,
                        IsCreated = data.IsCreated,
                        IsModified = data.IsModified
                    };
                    return userdata;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Logs in the specified user.
        /// </summary>
        /// <param name="login">The email address and the password.</param>
        /// <returns>
        /// returns the user information if login is successful
        /// </returns>
        public ResponseModel Login(Login login)
        {
            try
            {
                login.Password = EncodeDecode.EncodePassword(login.Password);
                var data = this._context.Users.FirstOrDefault(user => user.Email == login.Email && user.Passwrod == login.Password);
                if (data != null)
                {
                    var userdata = new ResponseModel()
                    {
                        Id = data.Id,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Email = data.Email,
                        IsActive = data.IsActive,
                        IsCreated = data.IsCreated,
                        IsModified = data.IsModified,
                        Type = data.Type,
                        UserRole = data.UserRole
                    };
                    return userdata;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// returns the user data
        /// </returns>
        public async Task<ResponseModel> Registration(RegistrationRequestModel user)
        {
            try
            {
                user.Passwrod = EncodeDecode.EncodePassword(user.Passwrod);
                UserDB dB = new UserDB()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Passwrod = user.Passwrod,
                    Type = user.Type,
                    IsActive = true,
                    IsCreated = DateTime.Now,
                    IsModified = DateTime.Now,
                    UserRole = "regular user"
                };
                _context.Users.Add(dB);
                await _context.SaveChangesAsync();

                ResponseModel responseModel = new ResponseModel()
                {
                    Id = dB.Id,
                    FirstName = dB.FirstName,
                    LastName = dB.LastName,
                    Email = dB.Email,
                    Type = dB.Type,
                    IsActive = dB.IsActive,
                    IsCreated = dB.IsCreated,
                    IsModified = dB.IsModified,
                    UserRole=dB.UserRole
                    
                };
                return responseModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The new password.</param>
        /// <returns>
        /// returns a true if password is successfully changed
        /// </returns>
        /// <exception cref="Exception">returns the exception</exception>
        public async Task<bool> ResetPassword(ResetPassword reset, int userId)
        {
            try
            {
                UserDB data = this._context.Users.FirstOrDefault(usr => usr.Id == userId);
                if (data != null)
                {
                    reset.Password = EncodeDecode.EncodePassword(reset.Password);
                    data.Passwrod = reset.Password;
                    data.IsModified = DateTime.Now;
                    var user = this._context.Users.Attach(data);
                    user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await this._context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
