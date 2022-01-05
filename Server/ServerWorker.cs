using Communication.Procedures;
using Communication.Procedures.Clients;
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
        public static (LoginResponse res, User? u) DoLogin(LoginRequest request, UserRole role)
        {
            using (Context context = new Context())
            {
                var users = context.Users.Include(u => u.Routes).ToList();
                User? user = users?.FirstOrDefault(x => x.Username == request.Username.ToLower() && x.Password == request.PasswordHash);

                if (user is null)
                    return (new LoginResponse(LoginState.BadPassword, Procedure.Void, request.GUID, ResponseState.Error, null, null), null);
                else if (user.IsBanned)
                    return (new LoginResponse(LoginState.UserBanned, Procedure.Void, request.GUID, ResponseState.Error, user.Name, null), null);
                else if (user.Role < role)
                    return (new LoginResponse(LoginState.UnsufficentRights, Procedure.Void, request.GUID, ResponseState.Error, user.Name, null), user);
                else
                {
                    byte[] token = Utils.GenerateToken(user);
                    user.Token = token.GetString();
                    user.TokenIssued = DateTime.Now;
                    context.SaveChanges();

                    return (new LoginResponse(LoginState.Success, token, request.GUID, ResponseState.Success, user.Name, GetRoutes(context, user)), user);
                }
            }
        }

        private static List<UserRoute> GetRoutes(Context context, User user)
        {
            List<UserRoute> routes = new();

            foreach (Route? route in user.Role == UserRole.Administrator ? context.Routes.ToList() : user.Routes)
            {
                if (route is null)
                    continue;

                bool isPrimary = user.Role != UserRole.Administrator;
                if (!isPrimary)
                    isPrimary = user.Routes.Any(x => x.Id == route.Id);

                routes.Add(new UserRoute(route.Id, route.Name, isPrimary));
            }

            return routes;
        }

        public static ClientsListResponse GetClients(ClientsListRequest request)
        {
            using (Context context = new Context())
            {
                (TokenState token, User? user) = context.CheckUser(request.Token, UserRole.User);

                if (token == TokenState.Expired)
                    return new ClientsListResponse(request.GUID, ResponseState.ExpiredToken, null);
                else if (token == TokenState.UnsufficentRights)
                    return new ClientsListResponse(request.GUID, ResponseState.UnsufficentRights, null);
                else if (token == TokenState.Ok)
                {
                    if (user is null)
                        return new ClientsListResponse(request.GUID, ResponseState.InvalidToken, null);

                    bool isAdmin = user.Role > UserRole.Manager;

                    List<D.ClientInfo> clients = new();
                    var list = context.Routes.Include(x => x.Users).Include(x => x.Clients).FirstOrDefault(x => x.Id == request.RouteId && (x.Users.Contains(user) || isAdmin))?.Clients;
                    if (list is null)
                        return new ClientsListResponse(request.GUID, ResponseState.UnsufficentRights, null);

                    foreach (var client in list)
                    {
                        bool occupied = client.Shifts.Any(x => x.StartTime is not null && x.EndTime is null);
                        clients.Add(new(client.Id, client.Name, !occupied));
                    }

                    return new ClientsListResponse(request.GUID, ResponseState.Success, clients);
                }
                else
                    return new ClientsListResponse(request.GUID, ResponseState.InvalidToken, null);
            }
        }
    }
}
