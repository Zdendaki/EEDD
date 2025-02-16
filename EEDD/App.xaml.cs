using Common.Data;
using EEDD.Controls;
using EEDD.Endpoint;
using System.Windows;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly uint DeviceId = DeviceIdHelper.GetDeviceId();

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

        internal static EddClient Client { get; set; } = null!;

        internal static Client ClientData { get; set; } = null!;

        internal static Guid Secret { get; set; } = Guid.Empty;

        internal static Route Route { get; set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Client?.DisconnectAndStop();
            base.OnExit(e);
        }
    }
}
