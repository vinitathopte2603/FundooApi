//-----------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.UserRequestModel
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

    /// <summary>
    /// properties of the fields to be returned as response after generation of token
    /// </summary>
    public class ResponseModel
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
        /// Gets or sets the created time and date of account.
        /// </summary>
        /// <value>
        /// The time on which account was created.
        /// </value>
        public DateTime IsCreated { get; set; }

        /// <summary>
        /// Gets or sets the modified time of the account.
        /// </summary>
        /// <value>
        /// the time and date of the last modification 
        /// </value>
        public DateTime IsModified { get; set; }
    }
    public class NoteResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
        public string Color { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime IsModified { get; set; }
        public List<LabelResponseModel> labels { get; set; }
    }
    public class LabelResponseModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime IsModified { get; set; }

    }
}
