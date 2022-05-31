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
            string input = (user.Password + DateTime.Now.Ticks.ToString() + user.Username).Scramble();
            byte[] buffer = Encoding.GetEncoding(1251).GetBytes(input);
            return GetSHA256(buffer);
        }

        internal static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
