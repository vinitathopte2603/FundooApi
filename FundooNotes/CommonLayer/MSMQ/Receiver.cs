using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.MSMQ
{
    public class Receiver
    {
        public static string ReceiveFromMsmq()
        {
            string path = @".\Private$\FundooApplication";

            MessageQueue messageQueueReceive;
            messageQueueReceive = new MessageQueue(path);
            Message message = messageQueueReceive.Receive();
            message.Formatter = new BinaryMessageFormatter();
            return message.Body.ToString();
        }
    }
}
