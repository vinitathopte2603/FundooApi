//-----------------------------------------------------------------------
// <copyright file="SendMail.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Listener
{
    using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

    /// <summary>
    /// sending email
    /// </summary>
    public class SendMail
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="email">The email.</param>
        /// <returns>returns a boolean value</returns>
        /// <exception cref="System.Exception">returns the exception</exception>
        public static bool SendEmail(string token, string email)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(token) || !string.IsNullOrWhiteSpace(email))
                {
                    MailMessage mailMessage = new MailMessage("fundooapplication@gmail.com", email);
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    mailMessage.Subject = "Forget Password Link";
                    string url = "http://localhost:3000/resetpassword/" + token;
                    mailMessage.Body = "Click link to reset password <br>" + url;
                    mailMessage.IsBodyHtml = true;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("fundooapplication@gmail.com", "Fundoo@123");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(mailMessage);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
