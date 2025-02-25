using System.Diagnostics.CodeAnalysis;

namespace Common.Data.Helper
{
    public record TrainTypeItem
    {
        public required TrainType Type { get; init; }

        public required string Name { get; init; }

        [SetsRequiredMembers]
        public TrainTypeItem(TrainType type)
        {
            Type = type;
            Name = TrainHelper.GetCategory(type);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
