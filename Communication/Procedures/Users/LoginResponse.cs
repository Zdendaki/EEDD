namespace Communication.Procedures.Users
{
    public class LoginResponse : Procedure, IResponse
    {
        public LoginState State { get; set; }

        public byte[] LoginToken { get; set; }

        public byte[] RequestGUID { get; set; }

        public string? Name { get; set; }

        public List<UserRoute>? Routes { get; set; }

        public ResponseState ResponseState { get; set; }

        public LoginResponse(LoginState state, byte[] loginToken, byte[] requestGuid, ResponseState resState, string? name, List<UserRoute>? routes) : base(ProcedureType.LoginResponse)
        {
            State = state;
            LoginToken = loginToken;
            RequestGUID = requestGuid;
            ResponseState = resState;
            Name = name;
            Routes = routes;
        }
    }
}
