namespace Communication.Procedures.Clients
{
    public class ClientDataRequest : Procedure
    {
        public int ShiftId { get; set; }

        public ClientDataRequest(byte[] token, int shiftId) : base(ProcedureType.ClientDataRequest)
        {
            Token = token;
            ShiftId = shiftId;
        }
    }
}
