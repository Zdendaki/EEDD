using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Procedures.Clients
{
    public class ClientDataRequest : Procedure
    {
        public int ClientId { get; set; }

        public ClientDataRequest(int clientId)
        {
            ClientId = clientId;
        }
    }
}
