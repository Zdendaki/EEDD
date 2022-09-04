using Communication.Data;

namespace Communication.Procedures
{
    public abstract class RowData
    {
        public int Id { get; set; }

        public RowType RowType { get; set; }

        public bool Archived { get; set; }

        public bool Cancelled { get; set; }

        public bool Complete { get; set; }

        protected RowData(int id, RowType rowType, bool archived)
        {
            Id = id;
            RowType = rowType;
            Archived = archived;
        }

        protected RowData()
        {

        }
    }
    
    public class MessageRow : RowData
    {
        public char? RowChar { get; set; }

        public string Caption { get; set; }

        public SingleRowValue<string> Message { get; set; }

        public SingleRowValue<string?> Note { get; set; }

        public RowColor Color { get; set; }

        public MessageRow(int id, RowType rowType, RowColor color, bool archived, char? rowChar, string caption, SingleRowValue<string> message, SingleRowValue<string?> note) : base(id, rowType, archived)
        {
            RowChar = rowChar;
            Caption = caption;
            Message = message;
            Note = note;
            Color = color;
        }
    }

    public class SingleTrainRow : RowData, ITrainRow
    {
        public TrainData? Train { get; set; }

        public int Number { get; set; }

        public TrainType Type { get; set; }

        public string Route { get; set; }

        public SingleRowValue<DateTime?> Announced { get; set; }

        public AcceptionState? AcceptionState { get; set; }

        public SingleRowValue<DateTime?> Accepted { get; set; }

        public SingleRowValue<DateTime?> PMD { get; set; }

        public SingleRowValue<DateTime?> ActualDeparture { get; set; }

        public SingleRowValue<RowTrackValue?> Track { get; set; }

        public SingleRowValue<SignallerValue?> Sig1 { get; set; }

        public SingleRowValue<SignallerValue?> Sig2 { get; set; }

        public SingleRowValue<SignallerValue?> Sig3 { get; set; }

        public SingleRowValue<SignallerValue?> Sig4 { get; set; }

        public SingleRowValue<SignallerValue?> Sig5 { get; set; }

        public SingleRowValue<SignallerValue?> Sig6 { get; set; }

        public SingleRowValue<DateTime?> Time { get; set; }

        public short? Delay { get; set; }

        public List<RowDelayValue> Delays { get; set; }

        public SingleRowValue<DateTime?> Departed { get; set; }

        public SingleRowValue<string?> Note { get; set; }

        public bool? Approval { get; set; }

        public string? Exceptions { get; set; }

        public short? SentMessages { get; set; }

        public SingleRowValue<string?> Comment { get; set; }

        public SingleTrainRow(int id, RowType rowType, bool archived, TrainData? train, int number, TrainType type, string route) : base(id, rowType, archived)
        {
            Train = train;
            Number = number;
            Type = type;
            Route = route;
        }
    }

    public class DoubleTrainRow : RowData, ITrainRow
    {
        public TrainData? Train { get; set; }

        public int NumberA { get; set; }

        public int NumberD { get; set; }

        public TrainType TypeA { get; set; }

        public TrainType TypeD { get; set; }

        public string RouteA { get; set; }

        public string RouteD { get; set; }

        public DoubleRowValue<DateTime?> Announced { get; set; }

        public AcceptionState? AcceptionState { get; set; }

        public DoubleRowValue<DateTime?> Accepted { get; set; }

        public DoubleRowValue<DateTime?> PMD { get; set; }

        public DoubleRowValue<DateTime?> ActualDeparture { get; set; }

        public DoubleRowValue<RowTrackValue?> Track { get; set; }

        public DoubleRowValue<SignallerValue?> Sig1 { get; set; }

        public DoubleRowValue<SignallerValue?> Sig2 { get; set; }

        public DoubleRowValue<SignallerValue?> Sig3 { get; set; }

        public DoubleRowValue<SignallerValue?> Sig4 { get; set; }

        public DoubleRowValue<SignallerValue?> Sig5 { get; set; }

        public DoubleRowValue<SignallerValue?> Sig6 { get; set; }

        public DoubleRowValue<DateTime?> Time { get; set; }

        public short? DelayA { get; set; }

        public short? DelayD { get; set; }

        public List<RowDelayValue> DelaysA { get; set; }

        public List<RowDelayValue> DelaysD { get; set; }

        public DoubleRowValue<DateTime?> Departed { get; set; }

        public DoubleRowValue<string?> Note { get; set; }

        public bool? ApprovalA { get; set; }

        public bool? ApprovalD { get; set; }

        public string? ExceptionsA { get; set; }

        public string? ExceptionsD { get; set; }

        public short? SentMessagesA { get; set; }

        public short? SentMessagesD { get; set; }

        public DoubleRowValue<string?> Comment { get; set; }

        public DoubleTrainRow(int id, RowType rowType, bool archived, TrainData? train, int numberA, int numberD, TrainType typeA, TrainType typeD, string routeA, string routeD) : base(id, rowType, archived)
        {
            Train = train;
            NumberA = numberA;
            NumberD = numberD;
            TypeA = typeA;
            TypeD = typeD;
            RouteA = routeA;
            RouteD = routeD;
        }
    }

    public class DoubleRowValue<T>
    {
        public T? AValue { get; set; }

        public DateTime? AChanged { get; set; }

        public T? DValue { get; set; }

        public DateTime? DChanged { get; set; }

        public DoubleRowValue(T? Avalue, DateTime? Achanged, T? Dvalue, DateTime? Dchanged)
        {
            AValue = Avalue;
            AChanged = Achanged;
            DValue = Dvalue;
            DChanged = Dchanged;
        }

        public static implicit operator DoubleRowValue<T>((T? Avalue, DateTime? Achanged, T? Dvalue, DateTime? Dchanged) val) => new(val.Avalue, val.Achanged, val.Dvalue, val.Dchanged);
        public static implicit operator DoubleRowValue<T>((T? Avalue, T? Dvalue) val) => new(val.Avalue, null, val.Dvalue, null);

        public static DoubleRowValue<T>? GetData(T? Avalue, DateTime? Achanged, T? Dvalue, DateTime? Dchanged)
        {
            if (Avalue is not null || Dvalue is not null)
                return new DoubleRowValue<T>(Avalue, Achanged, Dvalue, Dchanged);
            else
                return null;
        }
    }

    public class SingleRowValue<T>
    {
        public T Value { get; set; }

        public DateTime? Changed { get; set; }

        public SingleRowValue(T value, DateTime? changed)
        {
            Value = value!;
            Changed = changed;
        }

        public static implicit operator SingleRowValue<T>((T? value, DateTime? changed) val) => new(val.value!, val.changed);

        public static SingleRowValue<T>? GetData(T? value, DateTime? changed)
        {
            if (value is not null)
                return new SingleRowValue<T>(value, changed);
            else
                return null;
        }
    }

    public class SignallerValue
    {
        public int Id { get; set; }

        public StationData.Signaller Signaller { get; set; }

        public DateTime? Time { get; set; }

        public SignallerState Type { get; set; }

        public string? Name { get; set; }

        public SignallerValue(int id, StationData.Signaller signaller, DateTime? time, SignallerState type, string? name)
        {
            Id = id;
            Signaller = signaller;
            Time = time;
            Type = type;
            Name = name;
        }
    }

    public interface ITrainRow
    {
        TrainData? Train { get; set; }
    }

    public struct RowDelayValue
    {
        public int Id { get; set; }

        public DelayReason Reason { get; set; }

        public short Minutes { get; set; }

        public int? TrainNumber { get; set; }

        public string? Description { get; set; }

        public RowDelayValue(int id, DelayReason reason, short minutes, int? trainNumber, string? description)
        {
            Id = id;
            Reason = reason;
            Minutes = minutes;
            TrainNumber = trainNumber;
            Description = description;
        }
    }

    public struct RowTrackValue
    {
        public string Track { get; set; }

        public bool Occupied { get; set; }

        public RowTrackValue(string track, bool occupied)
        {
            Track = track;
            Occupied = occupied;
        }
    }
}
