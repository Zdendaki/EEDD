using Communication.Data;

namespace Communication.Procedures.Clients
{
    public class HandshakeResponse : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseState ResponseState { get; set; }

        public byte[] PublicKey { get; set; }

        public HandshakeResponse(byte[] requestGUID, ResponseState responseState, byte[] publicKey) : base(ProcedureType.HandshakeResponse)
        {
            RequestGUID = requestGUID;
            ResponseState = responseState;
            PublicKey = publicKey;
        }
    }
}
