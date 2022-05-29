using Communication;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.TCP
{
    internal class TCPServer : TcpServer
    {
        internal DiffieHellman Diffie;
        
        public TCPServer(IPAddress address, int port) : base(address, port)
        {
            Diffie = new DiffieHellman();
        }

        protected override TcpSession CreateSession() 
        { 
            return new EDDSession(this); 
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP server caught an error with code {error}");
        }
    }
}
