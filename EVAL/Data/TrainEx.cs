using Common.Data;
using System.Diagnostics.CodeAnalysis;

namespace EVAL.Data
{
    internal class TrainEx
    {
        public required Train Template { get; init; }

        public Guid ID => Template.ID;

        public int Number => Template.Number;

        public string FirstStop => Stops.First().Name;

        public string LastStop => Stops.Last().Name;

        public List<TrainStopEx> Stops { get; init; }

        [SetsRequiredMembers]
        public TrainEx(Train train)
        {
            Template = train;
            Stops = train.Stops.Select(s => new TrainStopEx(s)).ToList();
        }
    }
}
