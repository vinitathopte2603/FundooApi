//-----------------------------------------------------------------------
// <copyright file="LabelModel.cs" author="Vinita Thopte" company="Bridgelabz">
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
    /// properties of label
    /// </summary>
    public class LabelModel
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("Users")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the is created.
        /// </summary>
        /// <value>
        /// The is created.
        /// </value>
        public DateTime IsCreated { get; set; }

        /// <summary>
        /// Gets or sets the is modified.
        /// </summary>
        /// <value>
        /// The is modified.
        /// </value>
        public DateTime IsModified { get; set; }
    }
}
