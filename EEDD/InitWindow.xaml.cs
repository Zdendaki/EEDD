using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace EEDD
{
    /// <summary>
    /// Interakční logika pro InitWindow.xaml
    /// </summary>
    public partial class InitWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

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
                    Thread.Sleep(5000);

                    Dispatcher.Invoke(() =>
                    {
                        closing = true;
                        new MainWindow().Show();
                        Close();
                    });
                });
            }
            else
            {
                MainLabel.Content = "Probíhá připojování k serveru...";

                Task.Run(() =>
                {
                    try
                    {
                        if (!App.Client.ConnectAsync())
                            throw new Exception();

                        Dispatcher.Invoke(() =>
                        {
                            closing = true;
                            new LoginWindow().Show();
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
