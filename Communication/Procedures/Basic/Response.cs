namespace Communication.Procedures.Basic
{
    public class Response : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseMessage Message { get; set; }

        public Response(byte[] requestGUID, ResponseMessage message) : base(ProcedureType.Response)
        {
            RequestGUID = requestGUID;
            Message = message;
        }
    }

    public enum ResponseMessage
    {
        Ok,
        BadCredentials,
        Error
    }
}
