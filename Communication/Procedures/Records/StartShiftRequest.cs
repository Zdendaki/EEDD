using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Procedures.Records
{
    public class StartShiftRequest : Procedure
    {
        public int ClientId { get; set; }

        public StartShiftRequest(byte[] token, int clientId) : base(ProcedureType.StartShiftRequest)
        {
            Token = token;
            ClientId = clientId;
        }
    }
}
