//-----------------------------------------------------------------------
// <copyright file="MsmqSend.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.MSMQ
{
    using System;
    using Experimental.System.Messaging;

    /// <summary>
    /// Use of Microsoft messaging queue
    /// </summary>
    public class MsmqSend
    {
        /// <summary>
        /// MSMQs the send method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="email">The email.</param>
        public static void MsmqSendMethod(string token, string email)
        {
            MessageQueue messageQueue;
            string message = token;
            string path = @".\Private$\FundooApplication";
            try
            {
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                }
                else
                {
                    MessageQueue.Create(path);
                    messageQueue = new MessageQueue(path);
                }

                messageQueue.Label = "Fundoo mail sending";
                Message message1 = new Message(message);
                message1.Formatter = new BinaryMessageFormatter();
                messageQueue.Send(message1, email);
                Console.WriteLine("Token Sent");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
