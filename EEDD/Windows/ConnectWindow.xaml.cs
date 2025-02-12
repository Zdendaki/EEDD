using Common.Data;
using Common.Messages;
using Common.Messages.Data;
using Common.Messages.Login;
using EEDD.Endpoint;
using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace EEDD.Windows
{
    /// <summary>
    /// Interakční logika pro ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        bool _selectingTab = false;

        public ConnectWindow()
        {
            InitializeComponent();

            RouteSelect.ItemsSource = App.Routes;

            UpdateTab();
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

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (!ProcessPage())
                return;

            _selectingTab = true;
            if (Tabs.SelectedIndex < Tabs.Items.Count - 1)
                Tabs.SelectedIndex++;
            _selectingTab = false;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            _selectingTab = true;
            if (Tabs.SelectedIndex > 0)
                Tabs.SelectedIndex--;
            _selectingTab = false;
        }

        private void Client_MessageReceived(Message message)
        {
            if (message is RouteDataMessage routeData)
            {
                Dispatcher.Invoke(() =>
                {
                    StationSelect.ItemsSource = routeData.Route.Clients;
                    StationSelect.Items.Refresh();
                });
            }
        }

        private bool ProcessPage()
        {
            bool output;
            IsEnabled = false;
            switch (Tabs.SelectedIndex)
            {
                case 0:
                    output = ProcessServerConnect();
                    break;
                case 1:
                    output = ProcessLogin();
                    break;
                default:
                    output = false;
                    break;
            }
            IsEnabled = true;
            return output;
        }

        private bool ProcessServerConnect()
        {
            if (App.Client?.IsConnected == true)
                App.Client.DisconnectAndStop();

            if (!IPAddress.TryParse(ServerAddress.Text, out IPAddress? address))
            {
                MessageBox.Show(this, "Adresa serveru není platná.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!ushort.TryParse(ServerPort.Text, out ushort port))
            {
                MessageBox.Show(this, "Port není platný.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            App.Client = new EddClient(address, port);
            bool connected = App.Client.ConnectAsync();
            App.Client.MessageReceived += Client_MessageReceived;

            if (!connected)
            {
                MessageBox.Show(this, "Nepodařilo se připojit k serveru.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            App.Client.SendMessage(new DataRequestMessage(DataType.RoutesList));

            return true;
        }

        private bool ProcessLogin()
        {
            bool loggedIn = false;
            bool responded = false;
            bool badCredentials = false;

            void callback(ResponseMessage response)
            {
                if (response.Status == ResponseStatus.BadCredentials)
                    badCredentials = true;

                if (response.Status == ResponseStatus.Accepted)
                    loggedIn = true;

                responded = true;
            }

            if (RouteSelect.SelectedItem is not RouteBase route)
            {
                MessageBox.Show(this, "Vyberte trať.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(Username.Text))
            {
                MessageBox.Show(this, "Zadejte uživatelské jméno.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            App.Client.SendQueuedMessage(new LoginMessage
            {
                RouteID = route.ID,
                Username = Username.Text,
                Password = Password.Password
            }, callback);

            int i = 0;
            while (!responded && i < 100)
            {
                Thread.Sleep(100);
                i++;
            }

            if (badCredentials)
            {
                MessageBox.Show(this, "Nesprávné uživatelské jméno nebo heslo.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!responded)
            {
                MessageBox.Show(this, "Přihlášení se nezdařilo.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (loggedIn)
            {
                App.Client.SendMessage(new DataRequestMessage(DataType.Route));
            }

            return loggedIn;
        }

        private void RefreshStations_Click(object sender, RoutedEventArgs e)
        {
            App.Client.SendMessage(new DataRequestMessage(DataType.Route));
        }
    }
}
