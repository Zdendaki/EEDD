namespace Communication.Procedures
{
    public enum ProcedureType
    {
        Void,
        Ping,
        Pong,
        LoginRequest,
        LoginResponse,
        InitData,
        ArchiveRequest,
        Request,
        Response,
        ClientsListRequest,
        ClientsListResponse,
        ClientDataRequest,
        ClientDataResponse,
        StartShiftRequest,
        StartShiftResponse,
        NewTrainRequest,
        NewTrainResponse,
        TrainOfferRequest,
        TrainOfferResponse,
        HandshakeRequest,
        HandshakeResponse
    }
}
