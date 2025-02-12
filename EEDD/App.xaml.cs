using Common.Data;
using EEDD.Controls;
using EEDD.Endpoint;
using System.ComponentModel;
using System.Windows;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static EDDTextBox? selectedTextBox;
        internal static EDDTextBox? SelectedTextBox
        {
            get => selectedTextBox;
            set
            {
                if (selectedTextBox is not null)
                    selectedTextBox.HasFocus = false;
                selectedTextBox = value;
                if (selectedTextBox is not null)
                    selectedTextBox.HasFocus = true;
            }
        }

        internal static BindingList<RouteBase> Routes { get; } = [];

        internal static EddClient Client { get; set; } = null!;

        internal static Route Route { get; set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Client?.DisconnectAndStop();
            base.OnExit(e);
        }
    }
}
