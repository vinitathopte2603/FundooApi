using FundooCommonLayer.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FundooCommonLayer.MSMQ
{
    public class SendEmail
    {
        public static string SendMail(string token, ForgotPassword forgotPassword)
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
    }
}
