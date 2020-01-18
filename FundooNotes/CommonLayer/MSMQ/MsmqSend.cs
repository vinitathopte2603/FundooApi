using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.MSMQ
{
    public class MsmqSend
    {
        public static void MsmqSendMethod(string token)
        {
            MessageQueue messageQueue;
            string description = "This is demo queue";
            string message = token;
            string path = @".\Private$\FundooApplication";
            try
            {
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = description;
                }
                else
                {
                    MessageQueue.Create(path);
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = description;
                }
                Message message1 = new Message(message);
                message1.Formatter = new BinaryMessageFormatter();
                messageQueue.Send(message1);
                Console.WriteLine("Token Sent");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
