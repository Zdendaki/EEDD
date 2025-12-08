using MessagePack;

namespace Common.Data
{
    public class Archive
    {

    }

    [MessagePackObject]
    [Union(1, typeof(CommentRow))]
    public abstract class ArchiveRow
    {
        [Key(0)]
        public required int ID { get; init; }

        [Key(1)]
        public required DateTime Timestamp { get; init; }

        [Key(2)]
        public required uint User { get; init; }

        [Key(3)]
        public string? Comment { get; init; }

        [Key(4)]
        public bool IsCancelled { get; init; }

        [Key(5)]
        public bool IsDone { get; init; }
    }

    [MessagePackObject]
    public class CommentRow : ArchiveRow
    {
        [Key(10)]
        public string Category { get; init; }

        [Key(11)]
        public string Value { get; init; }
    }
}
