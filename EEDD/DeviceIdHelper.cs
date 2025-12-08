using DeviceId;
using System.IO;
using System.IO.Hashing;

namespace EEDD
{
    internal static class DeviceIdHelper
    {
        public static uint GetDeviceId()
        {
            return new DeviceIdBuilder()
                .OnWindows(windows => windows
                    .AddMotherboardSerialNumber()
                    .AddProcessorId()
                    .AddSystemDriveSerialNumber()
                    .AddWindowsProductId()
                    .AddWindowsDeviceId()
                    .AddMachineGuid())
                .ToCrc32();
        }

        private static uint ToCrc32(this DeviceIdBuilder builder)
        {
            using MemoryStream ms = new();
            using BinaryWriter bw = new(ms);

            foreach (var component in builder.Components)
            {
                bw.Write(component.Key);
                bw.Write(component.Value.GetValue());
            }

            byte[] crc32 = Crc32.Hash(ms.ToArray());
            return BitConverter.ToUInt32(crc32);
        }
    }
}
