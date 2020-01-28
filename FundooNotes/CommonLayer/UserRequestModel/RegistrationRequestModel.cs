//-----------------------------------------------------------------------
// <copyright file="RegistrationRequestModel.cs" author="Vinita Thopte" company="Bridgelabz">
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
    /// properties of registration model
    /// </summary>
    public class RegistrationRequestModel
    {
        /// <summary>
        /// the first name
        /// </summary>
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string FirstName { get; set; }

        /// <summary>
        /// the last name
        /// </summary>
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string LastName { get; set; }

        /// <summary>
        /// email address
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// the type of user
        /// </summary>
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string Type { get; set; }

        /// <summary>
        /// the password
        /// </summary>
        public string Passwrod { get; set; }

    }
}
