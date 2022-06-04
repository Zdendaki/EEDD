using Communication.Data;
using Communication.Procedures;

namespace ServerData.Database
{
    [Table("Rows")]
    public class Row
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public RowType RowType { get; set; }

        [Required]
        public virtual User ResponsibleUser { get; set; }

        [Required]
        public virtual Station Station { get; set; }

        [Required]
        public bool RowComplete { get; set; } = false;

        public virtual Train? Train { get; set; }

        [Required]
        [Precision(0)]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public bool Cancelled { get; set; } = false;

        public char? RowChar { get; set; }

        public string? Caption { get; set; }

        public virtual RowDataString? Message { get; set; }

        public int? NumberA { get; set; }

        public int? NumberD { get; set; }

        public TrainType? TypeA { get; set; }

        public TrainType? TypeD { get; set; }

        [MaxLength(5)]
        public string? RouteA { get; set; }
        
        [MaxLength(5)]
        public string? RouteD { get; set; }

        public virtual RowDataDate? AnnouncedA { get; set; }

        public virtual RowDataAcception? AnnouncedD { get; set; }

        public virtual RowDataDate? AcceptedA { get; set; }

        public virtual RowDataDate? AcceptedD { get; set; }

        public virtual RowDataDate? APMD { get; set; }

        public virtual RowDataDate? DPMD { get; set; }

        public virtual RowDataDate? ActualDepA { get; set; }

        public virtual RowDataDate? ActualDepD { get; set; }

        public virtual RowDataTrack? TrackA { get; set; }

        public virtual RowDataTrack? TrackD { get; set; }

        public virtual RowDataSignaller? Sig1A { get; set; }

        public virtual RowDataSignaller? Sig2A { get; set; }

        public virtual RowDataSignaller? Sig3A { get; set; }

        public virtual RowDataSignaller? Sig4A { get; set; }

        public virtual RowDataSignaller? Sig1D { get; set; }

        public virtual RowDataSignaller? Sig2D { get; set; }

        public virtual RowDataSignaller? Sig3D { get; set; }

        public virtual RowDataSignaller? Sig4D { get; set; }

        public virtual RowDataDate? Arrival { get; set; }

        public virtual RowDataDate? Departure { get; set; }

        public short? ADelay { get; set; }

        public short? DDelay { get; set; }

        public virtual List<RowDataDelay> DelaysA { get; set; } = new();

        public virtual List<RowDataDelay> DelaysD { get; set; } = new();

        public virtual RowDataDate? DepartedA { get; set; }

        public virtual RowDataDate? DepartedD { get; set; }

        public virtual RowDataString? NoteA { get; set; }

        public virtual RowDataString? NoteD { get; set; }

        public bool? ApprovalA { get; set; }

        public bool? ApprovalD { get; set; }

        [MaxLength(50)]
        public string? ExceptionsA { get; set; }

        [MaxLength(50)]
        public string? ExceptionsD { get; set; }

        public short? SentMessagesA { get; set; }

        public short? SentMessagesD { get; set; }

        public virtual RowDataString? CommentA { get; set; }

        public virtual RowDataString? CommentD { get; set; }
    }

    [Table("RowDataStrings")]
    public class RowDataString
    {
        [Key]
        public int Id { get; set; }

        public string Data { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public static implicit operator (string, DateTime)?(RowDataString? data) => data is not null ? (data.Data, data.Changed) : null;

        public SingleRowValue<string> GetValue()
        {
            return new(Data, Changed);
        }

        public static SingleRowValue<string?> GetValue(RowDataString? data)
        {
            return new(data?.Data, data?.Changed);
        }

        public static DoubleRowValue<string?> GetValue(RowDataString? arrival, RowDataString? departure)
        {
            return new(arrival?.Data, arrival?.Changed, departure?.Data, departure?.Changed);
        }
    }

    [Table("RowDataDates")]
    public class RowDataDate
    {
        [Key]
        public int Id { get; set; }

        [Precision(0)]
        public DateTime Data { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public static implicit operator (DateTime, DateTime)?(RowDataDate? data) => data is not null ? (data.Data, data.Changed) : null;

        public static SingleRowValue<DateTime?> GetValue(RowDataDate? data)
        {
            return new(data?.Data, data?.Changed);
        }

        public static DoubleRowValue<DateTime?> GetValue(RowDataDate? arrival, RowDataDate? departure)
        {
            return new(arrival?.Data, arrival?.Changed, departure?.Data, departure?.Changed);
        }
    }

    [Table("RowDataAcceptions")]
    public class RowDataAcception
    {
        [Key]
        public int Id { get; set; }

        [Precision(0)]
        public DateTime Data { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public AcceptionState State { get; set; }

        [Precision(0)]
        public DateTime Accepted { get; set; }

        public static SingleRowValue<DateTime?> GetValue(RowDataAcception? data)
        {
            return new(data?.Data, data?.Changed);
        }

        public static DoubleRowValue<DateTime?> GetValue(RowDataDate? arrival, RowDataAcception? departure)
        {
            return new(arrival?.Data, arrival?.Changed, departure?.Data, departure?.Changed);
        }
    }

    [Table("RowDataTracks")]
    public class RowDataTrack
    {
        [Key]
        public int Id { get; set; }

        public string Track { get; set; }

        public bool Occupied { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public static SingleRowValue<RowTrackValue?> GetValue(RowDataTrack? data)
        {
            RowTrackValue? val = null;

            if (data is not null)
                val = new(data.Track, data.Occupied);

            return new(val, data?.Changed);
        }

        public static DoubleRowValue<RowTrackValue?> GetValue(RowDataTrack? arrival, RowDataTrack? departure)
        {
            RowTrackValue? arr = null;
            RowTrackValue? dep = null;

            if (arrival is not null)
                arr = new RowTrackValue(arrival.Track, arrival.Occupied);
            if (departure is not null)
                dep = new RowTrackValue(departure.Track, departure.Occupied);

            return new(arr, arrival?.Changed, dep, departure?.Changed);
        }
    }

    [Table("RowDataDelays")]
    public class RowDataDelay
    {
        [Key]
        public int Id { get; set; }

        public DelayReason Reason { get; set; }

        public short Minutes { get; set; }

        public int? TrainNumber { get; set; }

        public string? Description { get; set; }
    }

    [Table("RowDataSignallers")]
    public class RowDataSignaller
    {
        [Key]
        public int Id { get; set; }

        public virtual Signaller Signaller { get; set; }

        [Precision(0)]
        public DateTime? Time { get; set; }

        [Precision(0)]
        public DateTime? Changed { get; set; }

        public SignallerState Type { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        public static SingleRowValue<SignallerValue?> GetValue(RowDataSignaller? data, StationData station)
        {
            SignallerValue? dat = null;

            if (data is not null)
                dat = new(data.Id, station.Signallers.First(x => x.Id == data.Signaller.Id), data.Time, data.Type, data.Name);

            return new(dat, data?.Changed);
        }

        public static DoubleRowValue<SignallerValue?> GetValue(RowDataSignaller? arrival, RowDataSignaller? departure, StationData station)
        {
            SignallerValue? arr = null;
            SignallerValue? dep = null;

            if (arrival is not null)
                arr = new SignallerValue(arrival.Id, station.Signallers.First(x => x.Id == arrival.Signaller.Id), arrival.Time, arrival.Type, arrival.Name);
            if (departure is not null)
                dep = new SignallerValue(departure.Id, station.Signallers.First(x => x.Id == departure.Signaller.Id), departure.Time, departure.Type, departure.Name);

            return new(arr, arrival?.Changed, dep, departure?.Changed);
        }
    }
}
