using Communication.Data;
using Communication.Procedures.Users;
using ISOR.Database;
using Microsoft.AspNetCore.SignalR;

namespace ISOR.Hubs
{
    public class EDDHub : Hub
    {
        Context database;
        ConfigurationManager config;

        public EDDHub(Context context/*, ConfigurationManager config*/)
        {
            database = context;
            //this.config = config;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            return new LoginResponse(LoginState.Success, new byte[5], new byte[5], ResponseState.Success, "ASDF", null);
        }
    }
}
