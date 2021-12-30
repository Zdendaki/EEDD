using Communication.Procedures;
using Communication.Procedures.Users;
using Microsoft.EntityFrameworkCore;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Communication.Procedures;

namespace Server
{
    internal static class ServerWorker
    {
        public static LoginResponse DoLogin(LoginRequest request)
        {
            using (Context context = new Context())
            {
                var users = context.Users.Include(u => u.Routes).ToList();
                
                User? user = users?.FirstOrDefault(x => x.Username == request.Username && x.Password == request.PasswordHash);

                if (user is null)
                    return new LoginResponse(LoginState.BadPassword, Procedure.Void, request.GUID, null);
                else
                {
                    byte[] token = Utils.GenerateToken(user);
                    user.Token = token.GetString();
                    user.TokenIssued = DateTime.Now;
                    context.SaveChanges();
                    
                    return new LoginResponse(LoginState.Success, token, request.GUID, GetRoutes(user, context));
                }
            }
        }

        private static List<UserRoute> GetRoutes(User user, Context context)
        {
            List<UserRoute> routes = new();

            foreach (Route? route in user.Role == UserRole.Administrator ? context.Routes.ToList() : user.Routes)
            {
                if (route is null)
                    continue;

                List<D.Client> clients = new();
                foreach (var client in route.Clients)
                {
                    bool occupied = client.Shifts.Any(x => x.StartTime is not null && x.EndTime is null);
                    clients.Add(new(client.Id, client.Name, !occupied));
                }

                bool isPrimary = user.Role != UserRole.Administrator;
                if (!isPrimary)
                    isPrimary = user.Routes.Any(x => x.Id == route.Id);

                routes.Add(new UserRoute(route.Id, route.Name, clients, isPrimary));
            }

            return routes;
        }


    }
}
