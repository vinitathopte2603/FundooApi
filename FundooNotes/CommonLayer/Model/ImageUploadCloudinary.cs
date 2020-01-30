using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class ImageUploadCloudinary
    {
        public static string AddPhoto(string photoStream)
        {
            var cloudinary = new Cloudinary(new Account("dchnedqfu", "351451528633721", "X9ycGPVj3LDr1Ag7uRQyz_BrL9Q"));

            ImageUploadResult result = cloudinary.Upload(new ImageUploadParams
            {
                File = new FileDescription(photoStream),
            });

            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return result.SecureUri.AbsoluteUri;
        }
    }
}
