namespace Communication.Procedures.Users
{
    public class LoginResponse : Procedure, IResponse
    {
        public LoginState State { get; set; }

        public byte[] LoginToken { get; set; }

        public byte[] RequestGUID { get; set; }

        public List<UserRoute>? Routes { get; set; }

        public LoginResponse(LoginState state, byte[] loginToken, byte[] requestGuid, List<UserRoute>? routes) : base(ProcedureType.LoginResponse)
        {
            State = state;
            LoginToken = loginToken;
            RequestGUID = requestGuid;
            Routes = routes;
        }
    }

    public enum LoginState
    {
        Success,
        BadPassword,
        UserBanned
    }
}
