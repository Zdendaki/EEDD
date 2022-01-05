namespace Communication.Procedures.Clients
{
    public class ClientsListRequest : Procedure
    {
        public int RouteId { get; set; }
        
        public ClientsListRequest(byte[] token, int routeId) : base(ProcedureType.ClientsListRequest)
        {
            Token = token;
            RouteId = routeId;
        }
    }
}
