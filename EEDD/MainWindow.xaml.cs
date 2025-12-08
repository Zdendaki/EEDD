using Common.Data;
using EEDD.Controls;
using EEDD.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool canClose = false;

        public MainWindow()
        {
            SnapsToDevicePixels = true;

            InitializeComponent();

            // Init window
            Title = $"DOPRAVNÍ DENÍK - [{App.ClientData.Name} - {App.ClientData.User!.Name}]";
            NightView.IsChecked = Settings.Default.NightView;
            NightView_Click(this, null!);

            InitStatusbar();
            InitScale();
            InitHeader();
            InitRows();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitScale();
        }

        private void InitStatusbar()
        {
            App.Client.ConnectionChanged += SetCommunication;

            SetCommunication(App.Client.IsConnected);
        }

        private void InitScale()
        {
            PresentationSource source = PresentationSource.FromVisual(this);

            if (source is not null)
            {
                double scaleX = source.CompositionTarget.TransformToDevice.M11;
                double scaleY = source.CompositionTarget.TransformToDevice.M22;

                TableScale.ScaleX = TableScale.ScaleY = Math.Max(scaleX, scaleY);
            }
        }

        private void InitHeader() => RowHeader.Init();

        private void InitRows()
        {
            Rows.Children.Add(new RowComment());
            Rows.Children.Add(new RowTrain(StationColor.Gray, true));
            Rows.Children.Add(new RowTrain(StationColor.Gray, false));
            Rows.Children.Add(new RowTrain(StationColor.Yellow, true));
            Rows.Children.Add(new RowTrain(StationColor.Yellow, false));
            Rows.Children.Add(new RowTrain(StationColor.Green, false));
        }

        private void LabelZoomIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TableScale.ScaleX *= 1.1d;
            TableScale.ScaleY *= 1.1d;
        }

        private void LabelZoomOut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TableScale.ScaleX /= 1.1d;
            TableScale.ScaleY /= 1.1d;
        }

        private void SetCommunication(bool connected)
        {
            Communication.Dispatcher.Invoke(() =>
            {
                if (connected)
                {
                    Communication.Foreground = Brushes.Black;
                    Communication.Background = Brushes.Transparent;
                }
                else
                {
                    Communication.Foreground = Brushes.White;
                    Communication.Background = Brushes.Red;
                }
            });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (canClose)
                return;

            e.Cancel = true;

            var result = MessageBox.Show(this, "Přejete si ukončit směnu?", "Ukončení směny", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool ctrl = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);

            if (e.Key == Key.Insert || (ctrl && e.Key == Key.N))
            {
                InsertLine(this, e);

                e.Handled = true;
            }
        }

        private void InsertLine(object sender, RoutedEventArgs e)
        {
            InsertRowWindow irw = new(this);
            irw.ShowDialog();
        }

        private void NightView_Click(object sender, RoutedEventArgs e)
        {
            Background = NightView.IsChecked ? Brushes.DimGray : (Brush)Brushes.White;
            Settings.Default.NightView = NightView.IsChecked;
            Settings.Default.Save();
        }
    }
}
