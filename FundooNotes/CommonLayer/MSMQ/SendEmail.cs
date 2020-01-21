//-----------------------------------------------------------------------
// <copyright file="SendEmail.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-------------------------------------------------------------------
namespace FundooCommonLayer.MSMQ
{
    using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
    using FundooCommonLayer.UserRequestModel;

    /// <summary>
    /// Sends email
    /// </summary>
    public class SendEmail
    {
        /// <summary>
        /// Sends the mail using SMTP protocol
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="forgotPassword">The forgot password.</param>
        /// <returns>returns the confirmation message</returns>
        public static string SendMail(string token, ForgotPassword forgotPassword)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("fundooapplication@gmail.com", forgotPassword.Email);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                mailMessage.Subject = "Forget Password Link";
                mailMessage.Body = "Click link to reset password <br>" + token;
                mailMessage.IsBodyHtml = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("fundooapplication@gmail.com", "Fundoo@123");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
                return "mail sent";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
