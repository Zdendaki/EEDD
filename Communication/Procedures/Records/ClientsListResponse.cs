namespace Communication.Procedures.Records
{
    public class ClientsListResponse : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseState ResponseState { get; set; }

        public List<ClientInfo>? Clients { get; set; }

        public ClientsListResponse(byte[] requestGuid, ResponseState state, List<ClientInfo>? clients) : base(ProcedureType.ClientsListResponse)
        {
            RequestGUID = requestGuid;
            ResponseState = state;
            Clients = clients;
        }
    }
}
