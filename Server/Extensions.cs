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
        internal static bool CheckLogin(this Context context, string token)
        {
            return context.Users.Any(x => x.Token == token && x.TokenIssued.HasValue && (DateTime.Now - (DateTime)x.TokenIssued).TotalDays <= 1d);
        }

        public static string GetString(this byte[] input)
        {
            return BitConverter.ToString(input).Replace("-", null).ToLower();
        }
    }
}
