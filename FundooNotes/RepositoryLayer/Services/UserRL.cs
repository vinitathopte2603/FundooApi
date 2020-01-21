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
            this._context = context;
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
            var data = this._context.Registration.FirstOrDefault(user => user.Email == forgotPassword.Email);
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

        /// <summary>
        /// Logs in the specified user.
        /// </summary>
        /// <param name="login">The email address and the password.</param>
        /// <returns>
        /// returns the user information if login is successful
        /// </returns>
        public ResponseModel Login(Login login)
        {
            login.Password = EncodeDecode.EncodePassword(login.Password);
            var data = this._context.Registration.FirstOrDefault(user => user.Email == login.Email && user.Passwrod == login.Password);
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

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// returns the user data
        /// </returns>
        public UserDB Registration(UserDB user)
        {
            user.Passwrod = EncodeDecode.EncodePassword(user.Passwrod);
            user.IsCreated = DateTime.Now;
            user.IsModified = DateTime.Now;
            this._context.Registration.Add(user);
            this._context.SaveChanges();
            return user;
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The new password.</param>
        /// <returns>
        /// returns a true if password is successfully changed
        /// </returns>
        /// <exception cref="Exception">returns the exception</exception>
        public bool ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                UserDB data = this._context.Registration.FirstOrDefault(usr => usr.Id == resetPassword.Id);
                if (data != null)
                {
                    resetPassword.Password = EncodeDecode.EncodePassword(resetPassword.Password);
                    data.Passwrod = resetPassword.Password;
                    data.IsModified = DateTime.Now;
                    var user = this._context.Registration.Attach(data);
                    user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this._context.SaveChanges();
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
