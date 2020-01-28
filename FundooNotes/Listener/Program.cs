//-----------------------------------------------------------------------
// <copyright file="Program.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Listener
{
 
using System;
    using Experimental.System.Messaging;
    /// <summary>
    /// Entry point for the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            string path = @".\Private$\FundooApplication";
            MSMQListener mSMQListener = new MSMQListener(path);
            mSMQListener.Start();
            Console.WriteLine("listen");
        }
    }
}
