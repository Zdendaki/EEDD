using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal static class Utils
    {
        public static byte[] GetSHA256(byte[] input)
        {
            return SHA256.Create().ComputeHash(input);
        }

        internal static byte[] GenerateToken(User user)
        {
            string input = ScrambleString(user.Password + DateTime.Now.Ticks.ToString() + user.Username);
            byte[] buffer = Encoding.GetEncoding(1251).GetBytes(input);
            return GetSHA256(buffer);
        }

        public static string ScrambleString(this string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
