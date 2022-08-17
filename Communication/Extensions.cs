namespace Communication
{
    public static class Extensions
    {
        /// <summary>
        /// Check if byte array starts with pattern
        /// </summary>
        /// <param name="array">Search array</param>
        /// <param name="pattern">Search pattern</param>
        /// <returns><see cref="true"/> if pattern found, otherwise <see cref="false"/></returns>
        public static bool StartsWith(this byte[] array, byte[] pattern)
        {
            return StartsWith(array, 0, pattern);
        }

        /// <summary>
        /// Check if byte array starts with pattern
        /// </summary>
        /// <param name="array">Search array</param>
        /// /// <param name="offset">Search array offset</param>
        /// <param name="pattern">Search pattern</param>
        /// <returns><see cref="true"/> if pattern found, otherwise <see cref="false"/></returns>
        public static bool StartsWith(this byte[] array, int offset, byte[] pattern)
        {
            if (array.Length + offset < pattern.Length)
                return false;
            else
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (array[i + offset] != pattern[i])
                        return false;
                }
                return true;
            }
        }

        public static byte[] Cut(this byte[] array, long offset, long size)
        {
            byte[] buffer = new byte[size];
            Array.Copy(array, offset, buffer, 0, size);
            return buffer;
        }

        public static bool Compare(this byte[] a1, byte[] a2)
        {
            if (a1 is null || a2 is null)
                return false;

            return ((ReadOnlySpan<byte>)a1).SequenceEqual((ReadOnlySpan<byte>)a2);
        }
    }
}
