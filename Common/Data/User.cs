using MessagePack;

namespace Common.Data
{
    [MessagePackObject]
    public class User
    {
        [Key(0)]
        public uint ID { get; init; }

        [Key(1)]
        public string Name { get; init; }
    }
}
