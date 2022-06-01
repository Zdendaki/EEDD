namespace Communication.Procedures.Clients
{
    public class ClientDataRequest : Procedure
    {
        public int ShiftId { get; set; }

        public ClientDataRequest(int shiftId)
        {
            ShiftId = shiftId;
        }
    }
}
