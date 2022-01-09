namespace ServerData
{
    public enum RowType : byte
    {
        Arrival,
        Departure,
        Both,
        ShortBlue,
        ShortRed,
        LongRed,
        LongBlue
    }

    public enum TrainType : byte
    {
        Ex,
        R,
        Sp,
        Os,
        Sv,
        Nex,
        Pn,
        Mn,
        Lv,
        Sluz,
        PMD,
        ND
    }

    public enum TrainContact : byte
    {
        None,
        GSMR,
        GSMP,
        TRST,
        TRSS
    }

    public enum UserRole : byte
    {
        User,
        Manager,
        Administrator
    }

    public enum AcceptionState : byte
    {
        None,
        Proposed,
        Accepted,
        Declined,
        Error
    }

    public enum DelayReason : byte
    {
        None,

    }
}
