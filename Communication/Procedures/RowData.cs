using Communication.Data;

namespace Communication.Procedures
{
    public class RowData
    {
        public int Id { get; set; }

        public int? TrainId { get; set; }

        public bool CanModify { get; set; }

        public RowType RowType { get; set; }

        public (string text, DateTime time)? Caption { get; set; }

        public (string text, DateTime time)? Message { get; set; }

        public RowValue<int> TrainNumber { get; set; }

        public RowValue<TrainType> TrainType { get; set; }

        public RowValue<string> Stations { get; set; }

        public RowValue<byte> TrackNumbers { get; set; }

        public RowValue<DateTime> Announcements { get; set; }

        public RowValue<DateTime> PMD { get; set; }

        public RowValue<DateTime> ActualDeparture { get; set; }

        public RowValue<DateTime> Signaller1 { get; set; }

        public RowValue<DateTime> Signaller2 { get; set; }

        public RowValue<DateTime> Signaller3 { get; set; }

        public RowValue<DateTime> Signaller4 { get; set; }

        public RowValue<DateTime> ArrivalDeparture { get; set; }

        public RowValue<int> Delay { get; set; }

        public RowValue<DateTime> Departed { get; set; }

        public RowValue<string> Description { get; set; }

        public RowValue<bool> Approval { get; set; }

        public RowValue<bool> Exceptions { get; set; }

        public RowValue<TrainContact> TrainContact { get; set; }

        public RowValue<short> InformationsSent { get; set; }

        public RowData(int id, int? trainId, bool canModify, RowType rowType, (string, DateTime)? caption, (string, DateTime)? message)
        {
            Id = id;
            TrainId = trainId;
            CanModify = canModify;
            RowType = rowType;
            Caption = caption;
            Message = message;
        }

        public RowData(int id, int? trainId, bool canModify, RowType rowType, RowValue<int> trainNumber, RowValue<TrainType> trainType, RowValue<string> stations, RowValue<byte> trackNumbers, RowValue<DateTime> announcements, RowValue<DateTime> pmd, RowValue<DateTime> actualDeparture, RowValue<DateTime> signaller1, RowValue<DateTime> signaller2, RowValue<DateTime> signaller3, RowValue<DateTime> signaller4, RowValue<DateTime> arrivalDeparture, RowValue<int> delay, RowValue<DateTime> departed, RowValue<string> description, RowValue<bool> approval, RowValue<bool> exceptions, RowValue<TrainContact> trainContact, RowValue<short> informationsSent)
        {
            Id = id;
            TrainId = trainId;
            CanModify = canModify;
            RowType = rowType;
            TrainNumber = trainNumber;
            TrainType = trainType;
            Stations = stations;
            TrackNumbers = trackNumbers;
            Announcements = announcements;
            PMD = pmd;
            ActualDeparture = actualDeparture;
            Signaller1 = signaller1;
            Signaller2 = signaller2;
            Signaller3 = signaller3;
            Signaller4 = signaller4;
            ArrivalDeparture = arrivalDeparture;
            Delay = delay;
            Departed = departed;
            Description = description;
            Approval = approval;
            Exceptions = exceptions;
            TrainContact = trainContact;
            InformationsSent = informationsSent;
        }

        public RowData() { }
    }

    public class RowValue<T>
    {
        public T? AValue { get; set; }

        public DateTime? AChanged { get; set; }

        public T? DValue { get; set; }

        public DateTime? DChanged { get; set; }

        public RowValue(T? Avalue, DateTime? Achanged, T? Dvalue, DateTime? Dchanged)
        {
            AValue = Avalue;
            AChanged = Achanged;
            DValue = Dvalue;
            DChanged = Dchanged;
        }

        public static implicit operator RowValue<T>((T? Avalue, DateTime? Achanged, T? Dvalue, DateTime? Dchanged) val) => new(val.Avalue, val.Achanged, val.Dvalue, val.Dchanged);
        public static implicit operator RowValue<T>((T? Avalue, T? Dvalue) val) => new(val.Avalue, null, val.Dvalue, null);
    }

    public class SignallerData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }
    }
}
