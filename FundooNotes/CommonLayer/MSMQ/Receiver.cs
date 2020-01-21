//-----------------------------------------------------------------------
// <copyright file="Receiver.cs" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.MSMQ
{
using System;
using System.Collections.Generic;
using System.Text;
using Experimental.System.Messaging;

    /// <summary>
    /// Receives the token from MSMQ
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// Receives from MSMQ.
        /// </summary>
        /// <returns>returns the received token value</returns>
        public static string ReceiveFromMsmq()
        {
            string path = @".\Private$\FundooApplication";
            try
            {
                MessageQueue messageQueueReceive;
                messageQueueReceive = new MessageQueue(path);
                Message message = messageQueueReceive.Receive();
                message.Formatter = new BinaryMessageFormatter();
                return message.Body.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
