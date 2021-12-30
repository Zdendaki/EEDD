namespace ServerData
{
    public enum RowType
    {
        Arrival,
        Departure,
        Both,
        ShortBlue,
        ShortRed,
        LongRed,
        LongBlue
    }

    public enum TrainType
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

    public enum TrainContact
    {
        None,
        GSMR,
        GSMP,
        TRST,
        TRSS
    }

    public enum UserRole
    {
        Administrator,
        Manager,
        User
    }
}
