//-----------------------------------------------------------------------
// <copyright file="MSMQListener.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Listener
{
using System;
using System.Collections.Generic;
using System.Text;
    using Experimental.System.Messaging;

    /// <summary>
    /// sending mail using delegate
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);

    /// <summary>
    /// Listener class
    /// </summary>
    public class MSMQListener
    {
        /// <summary>
        /// The listen
        /// </summary>
        private bool _listen;

        /// <summary>
        /// The message queue
        /// </summary>
        private MessageQueue messageQueue;

        /// <summary>
        /// Occurs when [message received].
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="MSMQListener"/> class.
        /// </summary>
        /// <param name="queuePath">The queue path.</param>
        public MSMQListener(string queuePath)
        {
            this.messageQueue = new MessageQueue(queuePath);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <exception cref="System.Exception">returns exception</exception>
        public void Start()
        {
            try
            {
                _listen = true;
                messageQueue.Formatter = new BinaryMessageFormatter();
                messageQueue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
                messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveComplete);
                this.StartListening();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <exception cref="System.Exception">returns exception</exception>
        public void Stop()
        {
            try
            {
                this._listen = false;
                this.messageQueue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
                this.messageQueue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveComplete);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Starts the listening.
        /// </summary>
        /// <exception cref="System.Exception">returns exception</exception>
        private void StartListening()
        {
            try
            {
                if (!_listen)
                {
                    return;
                }
                if (this.messageQueue.Transactional)
                {
                    this.messageQueue.BeginPeek();
                }
                else
                {
                    messageQueue.BeginReceive();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Called when [peek completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PeekCompletedEventArgs"/> instance containing the event data.</param>
        private void OnPeekCompleted(object sender, PeekCompletedEventArgs args)
        {
            messageQueue.EndPeek(args.AsyncResult);
            MessageQueueTransaction transaction = new MessageQueueTransaction();
            Message message = null;
            try
            {
                transaction.Begin();
                message = messageQueue.Receive(transaction);
                transaction.Commit();
                StartListening();
                FireReceiveEvent(message.Body);
            }
            catch
            {
                transaction.Abort();
            }
        }

        /// <summary>
        /// Fires the receive event.
        /// </summary>
        /// <param name="body">The body.</param>
        private void FireReceiveEvent(object body)
        {
            MessageReceived?.Invoke(this, new MessageEventArgs(body));
        }

        /// <summary>
        /// Called when [receive complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ReceiveCompletedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.Exception">returns the exception</exception>
        private void OnReceiveComplete(object sender, ReceiveCompletedEventArgs args)
        {
            try
            {
                Message msg = this.messageQueue.EndReceive(args.AsyncResult);
                Console.WriteLine(msg.Body.ToString() + " " + msg.Label.ToString());
                SendMail.SendEmail(msg.Body.ToString(), msg.Label.ToString());
                this.StartListening();
                this.FireReceiveEvent(msg.Body.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    /// <summary>
    /// message event class
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// The message body
        /// </summary>
        private object _messageBody;


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        public MessageEventArgs(object body)
        {
            this._messageBody = body;
        }

        /// <summary>
        /// Gets the message body.
        /// </summary>
        /// <value>
        /// The message body.
        /// </value>
        public object MessageBody
        {
            get { return this._messageBody; }
        }
    }
}
