//-----------------------------------------------------------------------
// <copyright file="UserBL.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Services
{
using System;
using System.Collections.Generic;
using System.Text;
    using System.Threading.Tasks;
    using FundooBusinessLayer.Interfaces;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.UserRequestModel;
    using FundooRepositoryLayer.Interfaces;
    using FundooRepositoryLayer.Services;

    /// <summary>
    /// implementation of User interface of business layer
    /// </summary>
    /// <seealso cref="FundooBusinessLayer.Interfaces.IUserBL" />
    public class UserBL : IUserBL
    {
        /// <summary>
        /// field declaration
        /// </summary>
        private readonly IUserRL _userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBL"/> class.
        /// </summary>
        /// <param name="userRL">The object declaration of user interface.</param>
        public UserBL(IUserRL userRL)
        {
            this._userRL = userRL;
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgotPassword">The email address.</param>
        /// <returns>
        /// returns the user data
        /// </returns>
        /// <exception cref="Exception">returns the exception</exception>
        public ResponseModel ForgotPassword(ForgotPassword forgotPassword)
        {
            try
            {
                var data = this._userRL.ForgotPassword(forgotPassword);
                if (data != null)
                {
                    return data;
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
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The email and password.</param>
        /// <returns>
        /// returns the user data if login in successful
        /// </returns>
        public ResponseModel Login(Login login)
        {
            try
            {
                var data = this._userRL.Login(login);
                if (data != null)
                {
                    return data;
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

        public string ProfilePicture(int userId, ImageUploadRequestModel imageUpload)
        {
            if (imageUpload != null)
            {
                return _userRL.ProfilePicture(userId, imageUpload);
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
        /// returns the data of user
        /// </returns>
        /// <exception cref="Exception">user is empty</exception>
        public async Task<ResponseModel> Registration(RegistrationRequestModel user)
        {
            try
            {
                if (user != null)
                {
                    return await _userRL.Registration(user);
                }
                else
                {
                    throw new Exception("user is empty");
                }
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
        /// returns a true if password is changed else returns false
        /// </returns>
        public async Task<bool> ResetPassword(ResetPassword resetPassword, int userId)
        {
            try
            {
                if (userId == 0 || resetPassword.Password == null)
                {
                    return false;
                }
                else
                {
                    return await this._userRL.ResetPassword(resetPassword, userId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
