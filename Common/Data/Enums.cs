namespace Common.Data
{
    /// <summary>
    /// Typ stanoviště
    /// </summary>
    public enum SignallerType : byte
    {
        /// <summary>
        /// Výpravčí, vede dopravní deník
        /// </summary>
        Dispatcher,
        /// <summary>
        /// Předvídaný odjezd ve stanici
        /// </summary>
        PODJ,
        /// <summary>
        /// Pro rušící posun a volnost a postavení vlakové cesty
        /// </summary>
        TrainRoutePrep,
        /// <summary>
        /// Vlak vjel/odjel celý
        /// </summary>
        TrainArrivedDepartured,
        /// <summary>
        /// Dopravna
        /// </summary>
        Station,
        /// <summary>
        /// Rušící posun zastaven
        /// </summary>
        ShuntingStopped,
        /// <summary>
        /// Pro vlak postaveno a volno
        /// </summary>
        RouteForTrainSetAndFree
    }

    /// <summary>
    /// Barva stanice
    /// </summary>
    public enum StationColor : byte
    {
        /// <summary>
        /// Šedá
        /// </summary>
        Gray,
        /// <summary>
        /// Zelená
        /// </summary>
        Green,
        /// <summary>
        /// Žlutá
        /// </summary>
        Yellow,
        /// <summary>
        /// Červená
        /// </summary>
        Red
    }

    /// <summary>
    /// Typ vlaku
    /// </summary>
    public enum TrainType : byte
    {
        /// <summary>
        /// Expresní vlak
        /// </summary>
        Ex,
        /// <summary>
        /// Rychlík
        /// </summary>
        R,
        /// <summary>
        /// Spěšný vlak
        /// </summary>
        Sp,
        /// <summary>
        /// Osobní vlak
        /// </summary>
        Os,
        /// <summary>
        /// Soupravový vlak
        /// </summary>
        Sv,
        /// <summary>
        /// Nákladní expres
        /// </summary>
        Nex,
        /// <summary>
        /// Průběžný nákladní vlak
        /// </summary>
        Pn,
        /// <summary>
        /// Manipulační vlak
        /// </summary>
        Mn,
        /// <summary>
        /// Lokomotivní vlak
        /// </summary>
        Lv,
        /// <summary>
        /// Služební vlak
        /// </summary>
        Sluz,
        /// <summary>
        /// Nutný pomocný vlak
        /// </summary>
        Pom,
        /// <summary>
        /// Posun mezi dopravnami
        /// </summary>
        PMD,
        /// <summary>
        /// Přestavná jízda
        /// </summary>
        PJ
    }

    [Flags]
    public enum ConnectionFlags : ushort
    {
        None = 0,
        Prijeti = 1,
        Odhlaska = 2
    }

    /// <summary>
    /// Typ události vlaku
    /// </summary>
    public enum EventType : byte
    {
        Arrival,
        Departure,
        Pass
    }

    /// <summary>
    /// Typ řádku
    /// </summary>
    public enum RowType : byte
    {
        /// <summary>
        /// Příjezd
        /// </summary>
        Arrival,
        /// <summary>
        /// Odjezd
        /// </summary>
        Departure
    }

    /// <summary>
    /// Důvod narušení jízdy vlaku
    /// </summary>
    public enum DelayReason : byte
    {
        /// <summary>
        /// Sestava jízdního řádu
        /// </summary>
        D0,
        /// <summary>
        /// Sestava vlaku provozovatelem dráhy
        /// </summary>
        D1,
        /// <summary>
        /// Závady v provozních procesech
        /// </summary>
        D2,
        /// <summary>
        /// Sled vlaků z důvodu chybného řízení provozu
        /// </summary>
        D3,
        /// <summary>
        /// Zpoždění vlaků zaviněné zaměstnanci provozu
        /// </summary>
        D4,
        /// <summary>
        /// Dispozice provozovatele dráhy, dispečera řízení provozu
        /// </summary>
        D9 = 9,
        /// <summary>
        /// Vliv staničních ZZ
        /// </summary>
        Z1 = 11,
        /// <summary>
        /// Vliv traťových ZZ
        /// </summary>
        Z2,
        /// <summary>
        /// Vliv přejezdových ZZ
        /// </summary>
        Z3,
        /// <summary>
        /// Vliv sdělovacích zařízení
        /// </summary>
        Z4,
        /// <summary>
        /// Vliv trakčního vedení a zásobování elektrickou energií
        /// </summary>
        Z5,
        /// <summary>
        /// Závady na železničním svršku a jiné traťové závady
        /// </summary>
        Z6,
        /// <summary>
        /// Závady staveb železničního spodku (mosty, tunely)
        /// </summary>
        Z7,
        /// <summary>
        /// Zpoždění zaviněné zaměstnanci infrastruktury
        /// </summary>
        Z8,
        /// <summary>
        /// Ostatní závady infrastruktury
        /// </summary>
        Z9,
        /// <summary>
        /// Vliv plánovaných výluk
        /// </summary>
        S1 = 21,
        /// <summary>
        /// Vliv neplánovaných výluk, pozdě zahájených a ukončených výluk
        /// </summary>
        S2,
        /// <summary>
        /// Omezení rychlosti z důvodu stavu koleje
        /// </summary>
        S3,
        /// <summary>
        /// Závady způsobené personálem
        /// </summary>
        S8 = 28,
        /// <summary>
        /// Ostatní závady ve výlukové činnosti
        /// </summary>
        S9,
        /// <summary>
        /// Zpoždění následujícím provozovatelem dráhy
        /// </summary>
        X1 = 31,
        /// <summary>
        /// Zpoždění předchozím provozovatelem dráhy
        /// </summary>
        X2,
        /// <summary>
        /// Pozdní doručení přepravních dokladů
        /// </summary>
        K1 = 41,
        /// <summary>
        /// Nakládka, vykládka
        /// </summary>
        K2,
        /// <summary>
        /// Prodloužení plánovaného pobytu, zvýšená frekvence cestujících
        /// </summary>
        K3,
        /// <summary>
        /// Úprava nákladu, zpoždění způsobené přepravovanou zásilkou
        /// </summary>
        K4,
        /// <summary>
        /// Dispozice dopravce
        /// </summary>
        K5,
        /// <summary>
        /// Zpoždění zaviněné doprovodem vlaku, komerčními zaměstnanci dopravce
        /// </summary>
        K6,
        /// <summary>
        /// Ostatní přepravní závady
        /// </summary>
        K9 = 49,
        /// <summary>
        /// Použití jiné řady hnacího vozidla, nedodržení řazení podle JŘ
        /// </summary>
        V0,
        /// <summary>
        /// Sestava vlaku dopravcem
        /// </summary>
        V1,
        /// <summary>
        /// Technické závady osobních vozů
        /// </summary>
        V2,
        /// <summary>
        /// Technické závady nákladních vozů
        /// </summary>
        V3,
        /// <summary>
        /// Technické závady hnacích vozidel
        /// </summary>
        V4,
        /// <summary>
        /// Zpoždění zaviněné zaměstnanci dopravce
        /// </summary>
        V5,
        /// <summary>
        /// Ostatní závady vozidel
        /// </summary>
        V9 = 59,
        /// <summary>
        /// Ihned nerozlišitelné důvody narušení (ostatní blíže nespecifikované události)
        /// </summary>
        O0,
        /// <summary>
        /// Vliv mimořádných událostí
        /// </summary>
        O1,
        /// <summary>
        /// Povětrnostní vlivy
        /// </summary>
        O2,
        /// <summary>
        /// Čekání na přípoj v rámci čekacích dob
        /// </summary>
        O3,
        /// <summary>
        /// Opatření státních orgánů
        /// </summary>
        O4,
        /// <summary>
        /// Zákrok policie, RZS, IZS, HZS
        /// </summary>
        O6 = 66,
        /// <summary>
        /// Stávka
        /// </summary>
        O7,
        /// <summary>
        /// Sled vlaků (křižování, předjíždění, provozní intervaly)
        /// </summary>
        O8,
        /// <summary>
        /// Obrat soupravy, hnacího vozidla, personálu
        /// </summary>
        O9
    }

    public enum EditMode
    {
        CanEdit,
        CanModify,
        Locked
    }

    public enum FieldType
    {
        Static,
        Number,
        String,
        Time
    }

    public enum AnnounceState
    {
        None,
        Announced,
        Accepted,
        Refused,
        Phone
    }
}
