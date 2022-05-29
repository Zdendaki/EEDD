using Communication.Data;

namespace Communication.Procedures.Clients
{
    public class StartShiftResponse : Procedure, IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseState ResponseState { get; set; }

        public bool ShiftStarted { get; set; }

        public int? ShiftId { get; set; }

        public StartShiftResponse(byte[] requestGUID, ResponseState responseState, bool shiftStarted, int? shiftId = null) : base(ProcedureType.StartShiftResponse)
        {
            RequestGUID = requestGUID;
            ResponseState = responseState;
            ShiftStarted = shiftStarted;
            ShiftId = shiftId;
        }
    }
}
