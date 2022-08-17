using Communication.Data;
using Communication.Procedures;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = Communication.Procedures;
using S = ServerData;

namespace Server
{
    internal static class Extensions
    {
        internal static TokenState CheckToken(this Context context, string token, UserRole role)
        {
            return CheckUser(context, token, role).token;
        }

        internal static TokenState CheckToken(this Context context, byte[] token, UserRole role)
        {
            return CheckUser(context, token, role).token;
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

        internal static (TokenState token, User? user) CheckUser(this Context context, string token, UserRole role)
        {
            User? user = context.GetUser(token);

            if (user is null || !user.TokenIssued.HasValue || user.IsBanned)
                return (TokenState.Invalid, user);
            else if ((DateTime.Now - user.TokenIssued.Value).TotalDays > 1)
                return (TokenState.Expired, user);
            else if (user.Role < role)
                return (TokenState.UnsufficentRights, user);
            else
                return (TokenState.Ok, user);
        }

        internal static (TokenState token, User? user) CheckUser(this Context context, byte[] token, UserRole role)
        {
            string buffer = BitConverter.ToString(token).ToLower().Replace("-", null);
            return CheckUser(context, buffer, role);
        }

        public static string GetString(this byte[] input)
        {
            return BitConverter.ToString(input).Replace("-", null).ToLower();
        }

        public static string Scramble(this string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        public static byte[] Cut(this byte[] array, long offset, long size)
        {
            byte[] buffer = new byte[size];
            Array.Copy(array, offset, buffer, 0, size);
            return buffer;
        }
    }
}
