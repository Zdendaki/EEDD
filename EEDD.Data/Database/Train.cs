namespace ServerData.Database
{
    [Table("Trains")]
    [Index(nameof(Number))]
    public class Train
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public List<Stop> Stops { get; set; } = new List<Stop>();

        public Route Route { get; set; }

        public Train()
        {

        }

        public Train(Timetable tt, TimetableTrain template, DateTime start)
        {
            Number = template.Number;
            Route = tt.Route;
            foreach (var stop in template.Stops)
            {
                if (stop is not null)
                    Stops.Add(new Stop(this, stop, start));
            }
        }
    }

    [Table("Stops")]
    public class Stop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TrainType TrainType { get; set; }

        [Precision(0)]
        public DateTime? Arrival { get; set; }

        [Precision(0)]
        public DateTime? Departure { get; set; }

        [ForeignKey("StationTrackId")]
        public Track? Track { get; set; }

        [Required]
        public Train Train { get; set; }

        [ForeignKey("FromId")]
        public RouteTrack? From { get; set; }

        [ForeignKey("ToId")]
        public RouteTrack? To { get; set; }

        public Stop()
        {

        }

        public Stop(Train train, TimetableStop template, DateTime start)
        {
            TrainType = template.TrainType;
            if (template.Arrival is not null)
                Arrival = start.AddMinutes(template.Arrival.Value);
            if (template.Departure is not null)
                Departure = start.AddMinutes(template.Departure.Value);
            Track = template.Track;
            Train = train;
            From = template.From;
            To = template.To;
        }
    }

    [Table("TimetableTrains")]
    [Index(nameof(Number))]
    public class TimetableTrain
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public List<TimetableStop> Stops { get; set; } = new();

        public Timetable Timetable { get; set; }
    }

    [Table("TimetableStops")]
    public class TimetableStop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TrainType TrainType { get; set; }

        public short? Arrival { get; set; }

        public short? Departure { get; set; }

        [ForeignKey("StationTrackId")]
        public Track? Track { get; set; }

        [Required]
        public TimetableTrain Train { get; set; }

        [ForeignKey("FromId")]
        public RouteTrack? From { get; set; }

        [ForeignKey("ToId")]
        public RouteTrack? To { get; set; }
    }
}
