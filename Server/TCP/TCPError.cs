using System.Net.Sockets;

namespace Server.TCP
{
    struct TCPError
    {
        public SocketError Error { get; init; }

        public DateTime Time { get; init; }

        public TCPError(SocketError error, DateTime time)
        {
            Error = error;
            Time = time;
        }

        public TCPError(SocketError error)
        {
            Error = error;
            Time = DateTime.Now;
        }
    }
}