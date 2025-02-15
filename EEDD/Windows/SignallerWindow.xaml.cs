using Common.Data;
using System.Windows;
using System.Windows.Controls;

namespace EEDD.Windows
{
    /// <summary>
    /// Interakční logika pro SignallerWindow.xaml
    /// </summary>
    public partial class SignallerWindow : Window
    {
        public SignallerType Type { get; }

        public SignallerWindow(Window owner, SignallerType type)
        {
            InitializeComponent();

            Owner = owner;
            Type = type;

            InitializeTitle();
            InitializeMenu();
        }

        [Obsolete("Designer only", true)]
        public SignallerWindow() : this(null!, SignallerType.TrainRoutePrep) { }

        private void InitializeTitle()
        {
            switch (Type)
            {
                case SignallerType.Dispatcher:
                    Title = "Výpravčí vede dopravní deník";
                    break;
                case SignallerType.PODJ:
                    Title = "Předvídaný odjezd";
                    break;
                case SignallerType.TrainRoutePrep:
                    Title = "Pro zastavení ruš.posunu a postavení VC";
                    break;
                case SignallerType.TrainArrivedDepartured:
                    Title = "Vlak vjel/odjel celý";
                    break;
                case SignallerType.Station:
                    Title = "Dopravna";
                    break;
                case SignallerType.ShuntingStopped:
                    Title = "Rušící posun zastaven";
                    break;
                case SignallerType.RouteForTrainSetAndFree:
                    Title = "Pro vlak postaveno a volno";
                    break;
            }
        }

        private void InitializeMenu()
        {
            if (Type == SignallerType.Dispatcher)
                Crossed.IsEnabled = true;

            switch (Type)
            {
                case SignallerType.Dispatcher:
                case SignallerType.PODJ:
                case SignallerType.TrainRoutePrep:
                case SignallerType.ShuntingStopped:
                case SignallerType.RouteForTrainSetAndFree:
                    Occupied.IsEnabled = true;
                    break;
            }
        }

        private void Time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
