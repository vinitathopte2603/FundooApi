//-----------------------------------------------------------------------
// <copyright file="IUserBL.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Interfaces
{
using System;
using System.Collections.Generic;
using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.UserRequestModel;

    /// <summary>
    /// Method declaration in interface
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns the data of user</returns>
        Task<ResponseModel> Registration(RegistrationRequestModel user);

        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The email and password.</param>
        /// <returns>returns the user data if login in successful</returns>
        ResponseModel Login(Login login);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgotPassword">The email address.</param>
        /// <returns>returns the user data </returns>
        ResponseModel ForgotPassword(ForgotPassword forgotPassword);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The new password.</param>
        /// <returns>returns a true if password is changed else returns false</returns>
        Task<bool> ResetPassword(ResetPassword password, int userId);
        string ProfilePicture(int userId, ImageUploadRequestModel imageUpload);
    }
}
