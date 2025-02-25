using System.Collections;

namespace Common.Data.Static
{
    public class TrainActions : IEnumerable<TrainAction>
    {
        public IEnumerator<TrainAction> GetEnumerator()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                yield return actions[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static readonly TrainAction[] actions =
             [
                new(99, "NVC", "Nástup nebo výstup cestujících (komerční zastavení)"),
                new(142, "TP", "Technická prohlídka"),
                new(143, "OZ", "Zastavuje od otevření zastávky"),
                new(144, "NSM", "Nástup osoby se sníženou mobilitou"),
                new(145, "VSM", "Výstup osoby se sníženou mobilitou"),
                new(135, "ÚDZ", "Všechny úkony dopravce zrušeny"),
                new(132, "ZČP", "Pobyt k vyrovnání časového posunu"),
                new(37, "ORZ", "Ošetření rostlin a zvířat"),
                new(15, "Man", "Manipulace s vozy"),
                new(38, "SNZ", "Ošetření snadnozkazitelného zboží"),
                new(39, "AU", "Administrativní úkony"),
                new(3, "SLD", "Zastavení ze služebních důvodů dopravce"),
                new(998, "NezvZ", "Nezveřejněné zastavení"),
                new(999, "DVD3", "Povinnost jednat v dopravně D3 jako vlak druhý"),
                new(126, "ČZV", "Čekání na zpožděné vlaky"),
                new(123, "Nást", "Zastavení jen pro nástup"),
                new(115, "OČP", "Odjezd v čase příjezdu"),
                new(113, "NNP", "Nečeká na žádné přípoje"),
                new(12, "Č/D", "Čištění / dezinfekce"),
                new(43, "TOP", "Předtápění"),
                new(114, "OPV", "Odjezd ihned po výstupu"),
                new(120, "ÚHV", "Úvrať hnacího vozidla"),
                new(112, "Post", "Manipulace s poštou"),
                new(117, "1/2", "Pobyt kratší než 1/2 min"),
                new(100, "VHV", "Přepřah HV"),
                new(121, "ÚVL", "Úvrať vlaku"),
                new(101, "Osa", "Výměna lokomotivní čety – osa "),
                new(104, "ÚZB", "Úplná zkouška brzdy "),
                new(105, "JZB", "Jednoduchá zkouška brzdy"),
                new(108, "Zás", "Manipulace se zásilkou"),
                new(109, "Clo", "Celní a pasové odbavení "),
                new(110, "Jiný", "Jiný důvod pobytu "),
                new(125, "DD", "Zastavení jen z dopravních důvodů"),
                new(118, "Bezp", "Pobyt z bezpečnostních důvodů"),
                new(130, "OPD3", "Ohlašovací povinnost na D3 nařízena"),
                new(27, "PLČ", "Přestávka LČ"),
                new(136, "ODPOS", "Vlak odjíždí z dopravního bodu cestou posunu"),
                new(137, "OVV", "Odjezd až po výpravě výpravčím"),
                new(138, "OKV", "Pravidelný vjezd na kolej obsazenou vozidly"),
                new(139, "OKVSV", "Pravidelný vjezd na kolej obsazenou vozidly – současné vjezdy"),
                new(140, "OK", "Pravidelný vjezd na obsazenou kolej"),
                new(141, "PRUJ", "Průjezd"),
                new(122, "Znam", "Zastavení jen na znamení"),
                new(124, "Výst", "Zastavení jen pro výstup")
            ];
    }
}

#if false
new(99, "NVC", "Nástup nebo výstup cestujících (komerční zastavení)"),
new(142, "TP", "Technická prohlídka"),
new(143, "OZ", "Zastavuje od otevření zastávky"),
new(144, "NSM", "Nástup osoby se sníženou mobilitou"),
new(145, "VSM", "Výstup osoby se sníženou mobilitou"),
new(135, "ÚDZ", "Všechny úkony dopravce zrušeny"),
new(132, "ZČP", "Pobyt k vyrovnání časového posunu"),
new(37, "ORZ", "Ošetření rostlin a zvířat"),
new(15, "Man", "Manipulace s vozy"),
new(38, "SNZ", "Ošetření snadnozkazitelného zboží"),
new(39, "AU", "Administrativní úkony"),
new(3, "SLD", "Zastavení ze služebních důvodů dopravce"),
new(998, "NezvZ", "Nezveřejněné zastavení"),
new(999, "DVD3", "Povinnost jednat v dopravně D3 jako vlak druhý"),
new(126, "ČZV", "Čekání na zpožděné vlaky"),
new(123, "Nást", "Zastavení jen pro nástup"),
new(115, "OČP", "Odjezd v čase příjezdu"),
new(113, "NNP", "Nečeká na žádné přípoje"),
new(12, "Č/D", "Čištění / dezinfekce"),
new(43, "TOP", "Předtápění"),
new(114, "OPV", "Odjezd ihned po výstupu"),
new(120, "ÚHV", "Úvrať hnacího vozidla"),
new(112, "Post", "Manipulace s poštou"),
new(117, "1/2", "Pobyt kratší než 1/2 min"),
new(100, "VHV", "Přepřah HV"),
new(121, "ÚVL", "Úvrať vlaku"),
new(101, "Osa", "Výměna lokomotivní čety – osa "),
new(104, "ÚZB", "Úplná zkouška brzdy "),
new(105, "JZB", "Jednoduchá zkouška brzdy"),
new(108, "Zás", "Manipulace se zásilkou"),
new(109, "Clo", "Celní a pasové odbavení "),
new(110, "Jiný", "Jiný důvod pobytu "),
new(125, "DD", "Zastavení jen z dopravních důvodů"),
new(118, "Bezp", "Pobyt z bezpečnostních důvodů"),
new(130, "OPD3", "Ohlašovací povinnost na D3 nařízena"),
new(27, "PLČ", "Přestávka LČ"),
new(136, "ODPOS", "Vlak odjíždí z dopravního bodu cestou posunu"),
new(137, "OVV", "Odjezd až po výpravě výpravčím"),
new(138, "OKV", "Pravidelný vjezd na kolej obsazenou vozidly"),
new(139, "OKVSV", "Pravidelný vjezd na kolej obsazenou vozidly – současné vjezdy"),
new(140, "OK", "Pravidelný vjezd na obsazenou kolej"),
new(141, "PRUJ", "Průjezd"),
new(122, "Znam", "Zastavení jen na znamení"),
new(124, "Výst", "Zastavení jen pro výstup"),


ZRUŠENO:
new(133, "KTP", "Konečná technická prohlídka"),
new(11, "11", "Kontrola nákladu"),
new(13, "13", "Přeložení nákladu v přístavu"),
new(14, "14", "Přeložení nákladu ve stanici, kde je změna rozchodu"),
new(21, "21", "Napájení / zalévání"),
new(22, "22", "Krmení"),
new(23, "23", "Dojení"),
new(24, "24", "Postřikování"),
new(25, "25", "Uzavření ventilačních záklopek"),
new(26, "26", "Otevření ventilačních záklopek"),
new(41, "41", "Kontrola teploty"),
new(42, "42", "Znovu zmražení"),
new(44, "44", "Kontrola správné funkce mechanického mrazícího zařízení"),
new(45, "45", "Doplnění paliva stroje"),
new(46, "46", "Přepnutí stroje na zapnuto nebo vypnuto"),
new(74, "74", "Vážení"),
new(76, "76", "Změna přepravní smlouvy"),
new(77, "77", "Podrobení se fytosanitární kontrole"),
new(0, "TUPD", "Testovací úkon PD"),
new(116, "D3", "Ohlašovací povinnost na D3 zrušena"),
#endif