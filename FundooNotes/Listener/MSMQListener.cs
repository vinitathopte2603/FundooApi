using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listener
{
    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);
    public class MSMQListener
    {
        private bool _listen;
        private MessageQueue messageQueue;
        public event MessageReceivedEventHandler MessageReceived;

        public MSMQListener(string queuePath)
        {
            this.messageQueue = new MessageQueue(queuePath);
        }
        public void Start()
        {
            try
            {
                _listen = true;
                messageQueue.Formatter = new BinaryMessageFormatter();
                messageQueue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
                messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveComplete);
                StartListening();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Stop()
        {
            try
            {
                _listen = false;
                messageQueue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
                messageQueue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveComplete);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void StartListening()
        {
            try
            {
                if (!_listen)
                {
                    return;
                }
                if (messageQueue.Transactional)
                {
                    messageQueue.BeginPeek();
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
        private void FireReceiveEvent(object body)
        {
            MessageReceived?.Invoke(this, new MessageEventArgs(body));
        }
        private void OnReceiveComplete(object sender, ReceiveCompletedEventArgs args)
        {
            try
            {
                Message msg = messageQueue.EndReceive(args.AsyncResult);
                Console.WriteLine(msg.Body.ToString() + " " + msg.Label.ToString());
                SendMail.SendEmail(msg.Body.ToString(), msg.Label.ToString());
                StartListening();
                FireReceiveEvent(msg.Body.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public class MessageEventArgs : EventArgs
    {
        private object _messageBody;

        public object MessageBody
        {
            get { return _messageBody; }
        }

        public MessageEventArgs(object body)
        {
            _messageBody = body;

        }
    }
}
