using Communication;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Server.TCP
{
    internal class WSServer : WsServer
    {
        public bool Encryption { get; set; }
        
        internal DiffieHellman Diffie;
        
        public WSServer(IPAddress address, int port, bool encryption = true) : base(address, port)
        {
            Diffie = new DiffieHellman(encryption);
            Encryption = encryption;
        }

        protected override WsSession CreateSession() 
        { 
            return new EDDSession(this); 
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"WS server caught an error with code {error}");
        }
    }
}
