namespace Communication.Procedures.Basic
{
    public class Pong : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public Pong(byte[] requestGuid) : base(ProcedureType.Pong)
        {
            RequestGUID = requestGuid;
        }
    }
}
