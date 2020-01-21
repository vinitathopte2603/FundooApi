//-----------------------------------------------------------------------
// <copyright file="EncodeDecode.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.Model
{
    using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// Encode the given password
    /// </summary>
    public class EncodeDecode
    {
        /// <summary>
        /// Encodes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>the encoded password string</returns>
        /// <exception cref="Exception">Error in encoding" + e.Message</exception>
        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodeData = Convert.ToBase64String(encData_byte);
                return encodeData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in encoding" + e.Message);
            }
        }
    }
}
