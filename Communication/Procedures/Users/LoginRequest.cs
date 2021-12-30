namespace Communication.Procedures.Users
{
    public class LoginRequest : Procedure
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public LoginRequest(string username, string passwordHash) : base(ProcedureType.LoginRequest)
        {
            Username = username;
            PasswordHash = passwordHash;
        }
    }
}
