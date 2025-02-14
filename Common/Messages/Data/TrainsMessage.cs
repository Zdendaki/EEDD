using MessagePack;

namespace Common.Messages.Data
{
    [MessagePackObject]
    public class TrainsMessage : Message
    {
        [Key(1)]
        public required List<Common.Data.Train> Trains { get; init; }
    }
}
