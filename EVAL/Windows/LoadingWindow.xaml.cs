using Common.Messages;
using Common.Messages.Data;
using Common.SSL;
using System.Windows;

namespace EVAL.Windows
{
    /// <summary>
    /// Interakční logika pro LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();

            Loaded += LoadingWindow_Loaded;
            App.Client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(MessageReceivedEventArgs e)
        {
            if (e.Message is RouteDataMessage routeData)
            {
                App.Client.MessageReceived -= Client_MessageReceived;
                App.Route = routeData.Route;
                e.Handled = true;

                Dispatcher.Invoke(() =>
                {
                    new MainWindow().Show();
                    Close();
                });
            }
        }

        private void LoadingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.Client.SendMessage(new DataRequestMessage(DataType.Route)))
            {
                MessageBox.Show(this, "Nepodařilo se stáhnout data ze serveru. Aplikace bude ukončena.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
        }
    }
}
