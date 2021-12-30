using Newtonsoft.Json;

namespace Communication.Procedures
{
    public abstract class Procedure
    {
        [JsonIgnore]
        public static readonly byte[] Void = new byte[32] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        [JsonIgnore]
        public static readonly byte[] Full = new byte[32] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 };

        public byte[] Token { get; set; } = new byte[32];

        public ProcedureType Type { get; set; }

        public byte[] GUID { get; set; }

        public Procedure(ProcedureType type)
        {
            Type = type;
            GUID = Guid.NewGuid().ToByteArray();
        }

        public Procedure() { }
    }
}
