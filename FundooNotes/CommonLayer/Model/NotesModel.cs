//-----------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.Model
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

    /// <summary>
    /// Entity model
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotesID { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [ForeignKey("Users")]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
       //// [RegularExpression(@"^#(?:[a-FA-F0-9]{3}){1,2}$")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool IsArchive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pin; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool IsPin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trash; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool IsTrash { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        public DateTime IsCreated { get; set; }

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        public DateTime IsModified { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public DateTime Reminder { get; set; }
    }
}
