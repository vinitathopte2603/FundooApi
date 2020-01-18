using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class EncodeDecode
    {
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
