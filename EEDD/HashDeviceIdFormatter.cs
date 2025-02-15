using DeviceId;
using System.IO.Hashing;
using System.Text;

namespace EEDD
{
    public class HashDeviceIdFormatter : IDeviceIdFormatter
    {
        public HashDeviceIdFormatter() { }

        public string GetDeviceId(IDictionary<string, IDeviceIdComponent> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components));
            }

            string hash = string.Join(',', components.OrderBy(x => x.Key).Select(x => x.Value.GetValue()).ToArray());

            if (hash.Replace(",", null).Length < 5)
            {
                throw new FormatException("DeviceId is too short.");
            }

            int seed = new Random().Next(1000000, int.MaxValue);
            Random random = new(seed);

            Crc32 hwid = new();
            Crc32 clid = new();

            hwid.Append(Encoding.UTF8.GetBytes(hash));

            uint hwidi = hwid.GetCurrentHashAsUInt32();
            uint clidi = clid.GetCurrentHashAsUInt32();

            return $"{hwidi ^ random.Next():X8}-{seed ^ clidi:X8}-{seed ^ random.Next():X8}";
        }
    }
}
