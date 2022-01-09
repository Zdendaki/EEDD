namespace Communication.Procedures.Clients
{
    public class ClientDataResponse : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseState ResponseState { get; set; }

        public ClientData? Data { get; set; }

        public ClientDataResponse(byte[] requestGUID, ResponseState responseState, ClientData? data) : base(ProcedureType.ClientDataResponse)
        {
            RequestGUID = requestGUID;
            ResponseState = responseState;
            Data = data;
        }
    }
}
