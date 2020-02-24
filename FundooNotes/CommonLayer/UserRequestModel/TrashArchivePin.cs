//-----------------------------------------------------------------------
// <copyright file="TrashArchivePin.cs" author="Vinita Thopte" company="Bridgelabz">
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
    /// request model for trash pin and archive note
    /// </summary>
    public class TrashArchivePin
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TrashArchivePin"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool value { get; set; }
    }

    /// <summary>
    /// request model for color
    /// </summary>
    public class ColourRequest
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$",ErrorMessage = "Enter color in correct format")]
        
        public string color { get; set; }
    }
}
