//-----------------------------------------------------------------------
// <copyright file="UserDB.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.Model
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

    /// <summary>
    /// properties if the user
    /// </summary>
    public class UserDB
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Passwrod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the created time on which the account was generated.
        /// </summary>
        /// <value>
        /// The time on which account was created
        /// </value>
        public DateTime IsCreated { get; set; }

        /// <summary>
        /// Gets or sets the modified time and date of the account.
        /// </summary>
        /// <value>
        /// The time of last modification done
        /// </value>
        public DateTime IsModified { get; set; }
    }
}
