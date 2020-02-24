//-----------------------------------------------------------------------
// <copyright file="ImageUploadRequestModel.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.UserRequestModel
{
    using Microsoft.AspNetCore.Http;
    using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// property declaration
    /// </summary>
    public class ImageUploadRequestModel
    {
        /// <summary>
        /// Gets or sets the image URL. 
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public IFormFile ImageUrl { get; set; }
    }
}
