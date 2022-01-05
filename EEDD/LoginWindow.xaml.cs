using Communication;
using Communication.Procedures;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EEDD
{
    /// <summary>
    /// Interakční logika pro LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MessageReceivedEventHandler? message = null;
        Timer timer;

        public LoginWindow()
        {
            InitializeComponent();

            timer = new Timer()
            {
                AutoReset = true,
                Interval = 5000,
                Enabled = false
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UserRoute? ur = null;
            Dispatcher.Invoke(() =>
            {
                ur = (UserRoute)RouteSelect.SelectedItem;
            });

            if (ur is not null && App.Token is not null)
            {
                Task.Run(() =>
                {
                    App.Client.SendMessage(new ClientsListRequest(App.Token, ur.Id));
                });
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Trim().Length == 0 || Password.Password.Trim().Length == 0)
            {
                MessageBox.Show("Musíte zadat uživatelské jméno i heslo.", "Nevyplněna povinná pole", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string password = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password.Password.Trim()))).ToLower().Replace("-", null);
                string username = Username.Text.Trim();

                message = (proc) =>
                {
                    if (proc.Type == ProcedureType.LoginResponse)
                    {
                        LoginResponse res = (LoginResponse)proc;
                        switch (res.State)
                        {
                            case LoginState.Success:
                                App.Token = res.LoginToken;
                                App.TokenGenerated = DateTime.Now;
                                App.UserName = res.Name;
                                Dispatcher.Invoke(() =>
                                {
                                    RoutePanel.IsEnabled = true;
                                    RouteSelect.ItemsSource = res.Routes;
                                    if (RouteSelect.Items.Count > 0)
                                        RouteSelect.SelectedIndex = 0;
                                });
                                break;
                            case LoginState.UnsufficentRights:
                                Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("Nemáte dostatečná práva k přihlášení do aplikace.", "Nedostatečná práva", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    LoginButton.IsEnabled = true;
                                });
                                break;
                            case LoginState.UserBanned:
                                Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("Uživatelský přístup vám byl zablokován.", "Uživatel zablokován", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    LoginButton.IsEnabled = true;
                                });
                                break;
                            case LoginState.BadPassword:
                                Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("Zadali jste nesprávné přihlašovací údaje.", "Nesprávné údaje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    LoginButton.IsEnabled = true;
                                });
                                break;
                        }
                    }
                    else if (proc.Type == ProcedureType.ClientsListResponse)
                    {
                        ClientsListResponse res = (ClientsListResponse)proc;
                        if (res.Clients is null)
                            res.ResponseState = ResponseState.Error;

                        Dispatcher.Invoke(() =>
                        {
                            switch (res.ResponseState)
                            {
                                case ResponseState.Success:
                                    StationPanel.IsEnabled = true;
                                    int selected = StationSelect.SelectedIndex;
                                    if (selected < 0 && res.Clients?.Count > 0)
                                        selected = 0;
                                    StationSelect.ItemsSource = res.Clients;
                                    StationSelect.SelectedIndex = selected;
                                    timer.Enabled = true;
                                    break;
                                default:
                                    StationPanel.IsEnabled = false;
                                    timer.Enabled = false;
                                    StationSelect.SelectedItem = null;
                                    StationSelect.ItemsSource = null;
                                    IsOccupied.Visibility = Visibility.Hidden;
                                    break;
                            }
                        });
                    }
                    else if (proc.Type == ProcedureType.StartShiftResponse)
                    {

                    }
                };

                App.Client.MessageReceived += message;

                LoginButton.IsEnabled = false;

                Task.Run(() =>
                {
                    App.Client.SendMessage(new LoginRequest(username, password));
                });
            }
        }

        private void RouteSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserRoute ur = (UserRoute)RouteSelect.SelectedItem;

            if (ur is not null && App.Token is not null)
            {
                Task.Run(() =>
                {
                    App.Client.SendMessage(new ClientsListRequest(App.Token, ur.Id));
                });
            }
        }

        private void StationSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientInfo client = (ClientInfo)StationSelect.SelectedItem;

            if (client is not null)
            {
                SelectStationButton.IsEnabled = client.Free;
                IsOccupied.Visibility = client.Free ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                SelectStationButton.IsEnabled = false;
                IsOccupied.Visibility = Visibility.Hidden;
            }
        }
    }
}
