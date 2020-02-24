//-----------------------------------------------------------------------
// <copyright file="ImageUploadCloudinary.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.Model
{
    using System;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// upload image 
    /// </summary>
    public class ImageUploadCloudinary
    {
        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photoStream">The photo stream.</param>
        /// <returns>returns the image url</returns>
        /// <exception cref="Exception">returns exception if any</exception>
        public static string AddPhoto(IFormFile photoStream)
        {
            var cloudinary = new Cloudinary(new Account("dchnedqfu", "351451528633721", "X9ycGPVj3LDr1Ag7uRQyz_BrL9Q"));
            var stream = photoStream.OpenReadStream();
            var name = photoStream.Name;
            ImageUploadResult result = cloudinary.Upload(new ImageUploadParams
            {
                File = new FileDescription(name, stream),
            });

            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
                
            return result.SecureUri.AbsoluteUri;
        }
    }
}
