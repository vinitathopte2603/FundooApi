//-----------------------------------------------------------------------
// <copyright file="Login.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.UserRequestModel
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

    /// <summary>
    /// properties of fields required during logging in
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Property of the field required if the user has forgotten the password
    /// </summary>
    public class ForgotPassword
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }

    /// <summary>
    /// Properties of the fields required during resetting the password
    /// </summary>
    public class ResetPassword
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        //[RegularExpression("@^(?!.([A-Za-z0-9])1{1})(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$%^&-]).{8,}$)")]
        public string Password { get; set; }
    }
}
