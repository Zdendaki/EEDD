namespace Communication.Data
{
    public enum LoginState : byte
    {
        Success,
        BadPassword,
        UserBanned,
        UnsufficentRights
    }

    public enum TokenState : byte
    {
        Ok,
        Invalid,
        Expired,
        UnsufficentRights
    }

    public enum ResponseState : byte
    {
        Success,
        InvalidToken,
        ExpiredToken,
        UnsufficentRights,
        Error
    }

    /// <summary>
    /// Route track default direction
    /// </summary>
    public enum DefaultDirection : byte
    {
        /// <summary>
        /// No default direction
        /// </summary>
        None,
        /// <summary>
        /// From primary to secondary
        /// </summary>
        PrimarySecondary,
        /// <summary>
        /// From secondary to primary
        /// </summary>
        SecondaryPrimary
    }

    /// <summary>
    /// Traťové zabezpečovací zařízení
    /// </summary>
    public enum RouteInterlocking : byte
    {
        /// <summary>
        /// Telefonické dorozumívání
        /// </summary>
        Tel,
        /// <summary>
        /// Bez TZZ
        /// </summary>
        TZZ0,
        /// <summary>
        /// TZZ jednosměrné liché
        /// </summary>
        TZZ1L,
        /// <summary>
        /// TZZ jednosměrné sudé
        /// </summary>
        TZZ1S,
        /// <summary>
        /// TZZ obousměrné
        /// </summary>
        TZZ2
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
        Departure,
        /// <summary>
        /// Příjezd/odjezd
        /// </summary>
        Both,
        /// <summary>
        /// Krátký modrý
        /// </summary>
        Blue,
        /// <summary>
        /// Krátký červený
        /// </summary>
        Red
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
        Yellow
    }

    /// <summary>
    /// Barva řádku s poznámkou
    /// </summary>
    public enum RowColor : byte
    {
        /// <summary>
        /// Červená
        /// </summary>
        Red,
        /// <summary>
        /// Modrá
        /// </summary>
        Blue
    }

    /// <summary>
    /// Typ zápisu stanoviště
    /// </summary>
    public enum SignallerState : byte
    {
        /// <summary>
        /// Čas
        /// </summary>
        Time,
        /// <summary>
        /// Obsazeno
        /// </summary>
        Occupied,
        /// <summary>
        /// Zaškrtnuto
        /// </summary>
        Crossed
    }

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
        AnnouncedDepartureStation,
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
        RouteForTrainSetAndFree,
        /// <summary>
        /// Pro předvídaný odjezd na trati
        /// </summary>
        AnnouncedDepartureRoute
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
        /// Náhradní doprava
        /// </summary>
        ND,
        /// <summary>
        /// Přestavná jízda
        /// </summary>
        PJ
    }

    /// <summary>
    /// Kontakt na vlak
    /// </summary>
    public enum TrainContact : byte
    {
        /// <summary>
        /// Bez kontaktu na vlak
        /// </summary>
        None,
        /// <summary>
        /// GSM-R
        /// </summary>
        GSMR,
        /// <summary>
        /// Veřejná GSM síť
        /// </summary>
        GSMP,
        /// <summary>
        /// Traťový rádiový systém
        /// </summary>
        TRST,
        /// <summary>
        /// Síť radiodispečerská simplexní
        /// </summary>
        SRDS
    }

    /// <summary>
    /// Uživatelská role
    /// </summary>
    public enum UserRole : byte
    {
        /// <summary>
        /// Dopravce
        /// </summary>
        Operator,
        /// <summary>
        /// Výpravčí
        /// </summary>
        Dispatcher,
        /// <summary>
        /// Vedoucí
        /// </summary>
        Manager,
        /// <summary>
        /// Administrátor
        /// </summary>
        Administrator = 10
    }

    /// <summary>
    /// Stav předvídaného odjezdu
    /// </summary>
    public enum AcceptionState : byte
    {
        /// <summary>
        /// Žádný
        /// </summary>
        None,
        /// <summary>
        /// Nabídnutý vlak
        /// </summary>
        Proposed,
        /// <summary>
        /// Přijatý vlak
        /// </summary>
        Accepted,
        /// <summary>
        /// Odmítnutý vlak
        /// </summary>
        Declined,
        /// <summary>
        /// Chyba
        /// </summary>
        Error
    }

    /// <summary>
    /// Typ události vlaku
    /// </summary>
    public enum EventType : byte
    {
        /// <summary>
        /// Aktivace vlaku
        /// </summary>
        TrainActivation,
        /// <summary>
        /// Připravenost
        /// </summary>
        TrainReady,
        /// <summary>
        /// Příjezd/odjezd/průjezd dopravním bodem
        /// </summary>
        TrainStop,
        /// <summary>
        /// Deaktivace vlaku
        /// </summary>
        TrainDeactivated,
        /// <summary>
        /// Ukončena jízda vlaku
        /// </summary>
        TrainEnded
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
}
