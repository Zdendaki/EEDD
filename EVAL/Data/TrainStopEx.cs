using Common;
using Common.Data;
using System.Diagnostics.CodeAnalysis;

namespace EVAL.Data
{
    internal class TrainStopEx
    {
        private const string FORMAT = "HH:mm:ss";

        public required TrainStop Template { get; init; }

        public uint ID => Template.ID;

        public string Name { get; }

        public string Type => TrainHelper.GetCategory(Template.Type);

        public string Arrival => Template.Arrival?.ToString(FORMAT) ?? string.Empty;

        public string Departure => Template.Departure?.ToString(FORMAT) ?? string.Empty;

        public string RouteArrival => Template.RouteTrackArrival ?? string.Empty;

        public string TrackArrival => Template.TrackArrival ?? string.Empty;

        public string TrackDeparture => Template.TrackDeparture ?? string.Empty;

        public string RouteDeparture => Template.RouteTrackDeparture ?? string.Empty;

        public int Actions => Template.Actions?.Count ?? 0;

        [SetsRequiredMembers]
        public TrainStopEx(TrainStop stop)
        {
            Template = stop;
            Name = GetName();
        }

        private string GetName()
        {
            return App.Route.Stations.FirstOrDefault(s => s.ID == ID)?.Name ?? "[neplatná stanice]";
        }
    }
}
