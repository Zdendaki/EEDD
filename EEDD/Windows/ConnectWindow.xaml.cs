using Common.Data;
using Common.Messages;
using Common.Messages.Data;
using Common.Messages.Login;
using Common.TCP;
using EEDD.Endpoint;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace EEDD.Windows
{
    /// <summary>
    /// Interakční logika pro ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        volatile bool _selectingTab = false;

        public ConnectWindow()
        {
            InitializeComponent();

            UpdateTab();

            Username.Text = Settings.Default.Username;
            Password.Password = Settings.Default.Password;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_selectingTab)
                e.Handled = true;
            else
                UpdateTab();
        }

        private void UpdateTab()
        {
            foreach (TabItem itm in Tabs.Items)
            {
                itm.IsEnabled = Tabs.SelectedItem == itm;
            }

            Previous.IsEnabled = Tabs.SelectedIndex > 0;
            Next.IsEnabled = Tabs.SelectedIndex < Tabs.Items.Count;
            Next.Content = Tabs.SelectedIndex < Tabs.Items.Count - 1 ? "Další" : "Dokončit";
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            if (!await ProcessPage())
                return;

            _selectingTab = true;
            Dispatcher.Invoke(() =>
            {
                if (Tabs.SelectedIndex < Tabs.Items.Count - 1)
                    Tabs.SelectedIndex++;
                else
                {
                    MainWindow mw = new();
                    mw.Show();
                    Close();
                }
            });
            _selectingTab = false;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            _selectingTab = true;
            if (Tabs.SelectedIndex > 0)
                Tabs.SelectedIndex--;
            _selectingTab = false;
        }

        private void Client_MessageReceived(MessageReceivedEventArgs e)
        {
            if (e.Message is RouteDataMessage routeData)
            {
                Dispatcher.Invoke(() =>
                {
                    StationSelect.ItemsSource = routeData.Route.Clients;
                    StationSelect.Items.Refresh();
                });
            }
            else if (e.Message is RoutesMessage routes)
            {
                Dispatcher.Invoke(() =>
                {
                    RouteSelect.ItemsSource = routes.Routes;
                    RouteSelect.Items.Refresh();

                    RouteSelect.SelectedItem = routes.Routes.FirstOrDefault(x => x.ID == Settings.Default.Route);
                });

                e.Handled = true;
            }
        }

        private async Task<bool> ProcessPage()
        {
            bool output;
            Dispatcher.Invoke(() => IsEnabled = false);
            int tab = Dispatcher.Invoke(() => Tabs.SelectedIndex);

            switch (tab)
            {
                case 0:
                    output = await ProcessServerConnect();
                    break;
                case 1:
                    output = await ProcessLogin();
                    break;
                case 2:
                    output = await ProcessClaim();
                    break;
                default:
                    output = false;
                    break;
            }

            Dispatcher.Invoke(() => IsEnabled = true);
            return output;
        }

        private async Task<bool> ProcessServerConnect()
        {
            if (App.Client?.IsConnected == true)
                App.Client.DisconnectAndStop();

            IPAddress? address = await ResolveIPAddress(ServerAddress.Text);

            if (address is null)
            {
                MessageBoxInvoke("Adresa serveru není platná.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!ushort.TryParse(ServerPort.Text, out ushort port))
            {
                MessageBoxInvoke("Port není platný.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            App.Client = new EddClient(address, port);
            bool connected = App.Client.ConnectAsync();
            App.Client.MessageReceived += Client_MessageReceived;

            int i = 0;
            while (!App.Client.IsConnected && App.Client.IsConnecting && connected && i < 1000)
            {
                await Task.Delay(100);
                i++;
            }

            if (!App.Client.IsConnected || !connected)
            {
                MessageBoxInvoke("Nepodařilo se připojit k serveru.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return App.Client.SendMessage(new DataRequestMessage(DataType.RoutesList));
        }

        private async Task<bool> ProcessLogin()
        {
            void fail() => MessageBoxInvoke("Přihlášení se nezdařilo.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

            if (RouteSelect.SelectedItem is not RouteBase route)
            {
                MessageBoxInvoke("Vyberte trať.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string username = Username.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBoxInvoke("Zadejte uživatelské jméno.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            LoginMessage login = new()
            {
                RouteID = route.ID,
                DeviceID = App.DeviceId,
                Username = username,
                Password = Password.Password
            };

            ResponseMessage? response = await App.Client.SendRequest(login, TimeSpan.FromSeconds(10));

            if (response is null)
            {
                fail();
                return false;
            }

            if (response.Status == ResponseStatus.BadCredentials)
            {
                MessageBoxInvoke("Nesprávné uživatelské jméno nebo heslo.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (response.Status != ResponseStatus.Accepted)
            {
                fail();
                return false;
            }

            Settings.Default.Route = route.ID;
            Settings.Default.Username = username;
            Settings.Default.Password = Password.Password;
            Settings.Default.Save();

            return App.Client.SendMessage(new DataRequestMessage(DataType.Route));
        }

        private async Task<bool> ProcessClaim()
        {
            void fail() => MessageBoxInvoke("Výběr stanice se neprovedl.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

            if (StationSelect.SelectedItem is not Client station)
            {
                MessageBoxInvoke("Vyberte stanici.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            ClaimClientMessage claim = new()
            {
                ClientID = station.ID
            };

            ResponseMessage? response = await App.Client.SendRequest(claim, TimeSpan.FromSeconds(10));

            if (response is null)
            {
                fail();
                return false;
            }

            if (response.Status == ResponseStatus.Refused)
            {
                switch (response.Message)
                {
                    case RefusedMessageHelper.NOTFOUND:
                        MessageBoxInvoke("Stanice nebyla nalezena.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case RefusedMessageHelper.OCCUPIED:
                        MessageBoxInvoke("Stanice je již obsazena.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    default:
                        fail();
                        break;
                }

                return false;
            }

            if (response.Status != ResponseStatus.Accepted)
            {
                fail();
                return false;
            }

            App.ClientData = station;
            App.ClientData.User = new(App.DeviceId, Username.Text);

            return App.Client.SendMessage(new DataRequestMessage(DataType.Trains));
        }

        private async Task<IPAddress?> ResolveIPAddress(string input)
        {
            IPAddress? address;

            if (!IPAddress.TryParse(input, out address))
            {
                try
                {
                    IPAddress[] addresses = await Dns.GetHostAddressesAsync(input);

                    if (addresses.Length > 0)
                        address = addresses[0];
                }
                catch { }
            }

            return address;
        }

        private void RefreshStations_Click(object sender, RoutedEventArgs e)
        {
            App.Client.SendMessage(new DataRequestMessage(DataType.Route));
        }

        protected override void OnClosed(EventArgs e)
        {
            if (App.Client is not null)
                App.Client.MessageReceived -= Client_MessageReceived;

            base.OnClosed(e);
        }

        private MessageBoxResult MessageBoxInvoke(string text, string caption, MessageBoxButton button, MessageBoxImage image)
        {
            return Dispatcher.Invoke(() => MessageBox.Show(this, text, caption, button, image));
        }
    }
}
