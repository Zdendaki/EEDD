using Common.Messages;
using System;

namespace EEDD.Endpoint
{
    internal class MessageReceivedEventArgs : EventArgs
    {
        public Message Message { get; }

        public bool Handled { get; set; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = message;
            Handled = false;
        }
    }
}
