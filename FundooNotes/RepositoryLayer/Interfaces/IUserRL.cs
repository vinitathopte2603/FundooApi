//-----------------------------------------------------------------------
// <copyright file="IUserRL.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Interfaces
{
using System;
using System.Collections.Generic;
using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.UserRequestModel;

    /// <summary>
    /// method declaration
    /// </summary>
    public interface IUserRL
    {
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns the user data</returns>
        Task<ResponseModel> Registration(RegistrationRequestModel user);

        /// <summary>
        /// Logs in the specified user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>returns the user information if login is successful</returns>
        ResponseModel Login(Login login);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgotPassword"> email address.</param>
        /// <returns>returns the user data to verify before resetting new password</returns>
        ResponseModel ForgotPassword(ForgotPassword forgotPassword);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>returns a true if password is successfully changed </returns>
        Task<bool> ResetPassword(ResetPassword reset, int userId);
    }
}
