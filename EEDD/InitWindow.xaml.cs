using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace EEDD
{
    /// <summary>
    /// Interakční logika pro InitWindow.xaml
    /// </summary>
    public partial class InitWindow : Window
    {
        [LibraryImport("user32.dll", SetLastError = true)]
        private static partial int GetWindowLong(IntPtr hWnd, int nIndex);

        [LibraryImport("user32.dll")]
        private static partial int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        bool downloading;
        bool closing = false;

        public InitWindow(bool dl)
        {
            InitializeComponent();

            downloading = dl;

        }

        public InitWindow() : this(false) { }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, -16, GetWindowLong(hwnd, -16) & ~0x80000);


            if (downloading)
            {
                MainLabel.Content = "Probíhá stahování dat stanice...";

                Task.Run(() =>
                {
                    /*App.Client.SendMessage(new ClientDataRequest(App.Token!, App.Data.ShiftId));

                    App.Client.MessageReceived += (proc) =>
                    {
                        Task.Run(() =>
                        {
                            Thread.Sleep(60000);
                            if (!downloaded)
                            {
                                MessageBox.Show(this, "Nepodařilo se stáhnout data ve stanoveném čase. Aplikace bude ukončena.", "Stahování dat se nezdařilo.", MessageBoxButton.OK, MessageBoxImage.Error);
                                Environment.Exit(0);
                            }
                        });


                        if (proc is ClientDataResponse)
                        {
                            ClientDataResponse res = (ClientDataResponse)proc;

                            if (res.ResponseState == ResponseState.Success && res.Data is not null && !downloaded)
                            {
                                App.Data.Client = res.Data;
                                App.Data.Signallers = res.Data.Stations.SelectMany(x => x.Signallers).DistinctBy(x => x.Id).OrderBy(x => x.Order).GroupBy(x => (x.Name, x.Type, x.Comment)).ToList();

                                downloaded = true;

                                Dispatcher.Invoke(() =>
                                {
                                    closing = true;
                                    new MainWindow().Show();
                                    Close();
                                });
                            }
                            else
                            {
                                MessageBox.Show(this, "Nepodařilo se stáhnout data stanice. Aplikace bude ukončena.", "Stahování dat se nezdařilo", MessageBoxButton.OK, MessageBoxImage.Error);
                                App.Client.SendMessage(new EndShiftRequest(App.Data.ShiftId));
                                closing = true;
                                Environment.Exit(0);
                            }
                        }
                    };*/
                });
            }
            else
            {
                MainLabel.Content = "Probíhá připojování k serveru...";

                Task.Run(() =>
                {
                    try
                    {
                        /*if (!App.Client.ConnectAsync())
                            throw new Exception();

                        while (App.Client.IsConnecting)
                            Thread.Sleep(100);

                        if (!App.Client.IsConnected)
                            throw new Exception();*/

                        Dispatcher.Invoke(() =>
                        {
                            closing = true;
                            Close();
                        });
                    }
                    catch
                    {
                        closing = true;
                        Dispatcher.Invoke(() => MessageBox.Show("Připojení k serveru dopravního deníku se nezdařilo. Kontaktujte administrátora aplikace.", "Připojení nelze navázat", MessageBoxButton.OK, MessageBoxImage.Error));
                        Environment.Exit(0);
                    }
                });
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!closing)
                e.Cancel = true;
        }
    }
}
