using Communication.Procedures;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal static class Extensions
    {
        internal static TokenState CheckToken(this Context context, string token, UserRole role)
        {
            User? user = context.GetUser(token);

            if (user is null || !user.TokenIssued.HasValue || user.IsBanned)
                return TokenState.Invalid;
            else if ((DateTime.Now - user.TokenIssued.Value).TotalDays > 1)
                return TokenState.Expired;
            else if (user.Role < role)
                return TokenState.UnsufficentRights;
            else
                return TokenState.Ok;
        }

        internal static TokenState CheckToken(this Context context, byte[] token, UserRole role)
        {
            string buffer = BitConverter.ToString(token).ToLower().Replace("-", null);
            return CheckToken(context, buffer, role);
        }

        internal static User? GetUser(this Context context, string token)
        {
            return context.Users.FirstOrDefault(x => x.Token == token);
        }

        internal static User? GetUser(this Context context, byte[] token)
        {
            string buffer = BitConverter.ToString(token).ToLower().Replace("-", null);
            return context.Users.FirstOrDefault(x => x.Token == buffer);
        }

        public static string GetString(this byte[] input)
        {
            return BitConverter.ToString(input).Replace("-", null).ToLower();
        }

        public static string Scramble(this string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
