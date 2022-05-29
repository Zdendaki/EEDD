namespace Communication.Procedures.Clients
{
    public class HandshakeRequest : Procedure
    {
        public byte[] PublicKey { get; set; }

        public HandshakeRequest(byte[] publicKey) : base(ProcedureType.HandshakeRequest)
        {
            PublicKey = publicKey;
        }
    }
}
