using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages
{
    [MessagePackObject]
    public class DataRequestMessage : Message
    {
        [Key(1)]
        public required DataType DataType { get; init; }

        public DataRequestMessage() : base() { }

        [SetsRequiredMembers]
        public DataRequestMessage(DataType type) : base()
        {
            DataType = type;
        }
    }

    public enum DataType : byte
    {
        RoutesList,
        Route,
        Trains
    }
}
