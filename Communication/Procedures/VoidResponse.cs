namespace Communication.Procedures
{
    public sealed class VoidResponse : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseState ResponseState { get; set; }

        public VoidResponse() : base() { }
    }
}
