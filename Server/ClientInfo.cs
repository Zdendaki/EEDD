using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ClientInfo
    {
        public Guid GUID { get; set; }

        public byte[] PublicKey { get; set; } = new byte[158];

        public User? User { get; set; }

        public Shift? Shift { get; set; }

        public ClientInfo(Guid guid)
        {
            GUID = guid;
        }

        public override int GetHashCode()
        {
            return GUID.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not ClientInfo)
                return false;
            else
                return (obj as ClientInfo)!.GUID == GUID;
        }
    }
}
