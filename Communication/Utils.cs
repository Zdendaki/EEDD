namespace Communication
{
    static class Utils
    {
        internal static byte[] Combine(params byte[][] arrays)
        {
            if (arrays.LongLength > 1)
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
            else
                return arrays[0];
        }

        public static bool Compare(byte[]? a1, byte[]? a2)
        {
            if (a1 is null || a2 is null)
                return false;

            return ((ReadOnlySpan<byte>)a1).SequenceEqual((ReadOnlySpan<byte>)a2);
        }
    }
}
