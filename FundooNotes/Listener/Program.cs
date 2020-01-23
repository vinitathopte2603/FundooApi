using Experimental.System.Messaging;
using System;

namespace Listener
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string path = @".\Private$\FundooApplication";
            MSMQListener mSMQListener = new MSMQListener(path);
            mSMQListener.Start();
            Console.WriteLine("listen");
        }
    }
}
