using System;
using System.Threading.Tasks;
using System.Windows;

namespace Manager
{
    /// <summary>
    /// Interakční logika pro InitWindow.xaml
    /// </summary>
    public partial class InitWindow : Window
    {
        public InitWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    App.Client.Connect();

                    if (!App.Client.Connected)
                        throw new Exception();

                    Dispatcher.Invoke(() =>
                    {
                        new LoginWindow().Show();
                        Close();
                    });
                }
                catch
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Připojení k serveru dopravního deníku se nezdařilo. Kontaktujte administrátora aplikace.", "Připojení nelze navázat", MessageBoxButton.OK, MessageBoxImage.Error));
                    Environment.Exit(0);
                }
            });
        }
    }
}
