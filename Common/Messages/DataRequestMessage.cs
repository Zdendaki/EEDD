using MessagePack;

namespace Common.Messages
{
    [MessagePackObject]
    public class DataRequestMessage : Message
    {
        [Key(1)]
        public DataType DataType { get; init; }
    }

    public enum DataType : byte
    {

    }
}
