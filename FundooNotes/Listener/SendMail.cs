using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Listener
{
    public class SendMail
    {
        public static bool SendEmail(string token, string email)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(token) || !string.IsNullOrWhiteSpace(email))
                {
                    MailMessage mailMessage = new MailMessage("fundooapplication@gmail.com", email);
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    mailMessage.Subject = "Forget Password Link";
                    mailMessage.Body = "Click link to reset password <br>" + token;
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
