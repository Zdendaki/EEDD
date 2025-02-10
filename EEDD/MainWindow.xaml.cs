using EEDD.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static EDDControl? focused = null;
        public static EDDControl? FocusedControl
        {
            get => focused;
            set
            {
                if (focused is not null)
                    focused.HasFocus = false;
                focused = value;
            }
        }

        bool canClose = false;

        public MainWindow()
        {
            InitializeComponent();

            //client = App.Data.Client;

            // Init window
            //Title = $"DOPRAVNÍ DENÍK - [{client.Name} - {client.User.Name}]";
            TableScale.ScaleX = TableScale.ScaleY = 1d;
            //InitHeader();
            InitRows();
        }

        private void InitRows()
        {
            Rows.Children.Add(new RowComment());
            Rows.Children.Add(new RowTrainDouble());
            Rows.Children.Add(new RowTrainDouble());
            Rows.Children.Add(new RowTrainDouble());
        }

        private void InitScale()
        {
            PresentationSource source = PresentationSource.FromVisual(this);

            if (source is not null)
            {
                double scaleX = 1d + (source.CompositionTarget.TransformToDevice.M11 - 1d) / 2d;
                double scaleY = 1d + (source.CompositionTarget.TransformToDevice.M22 - 1d) / 2d;

                TableScale.ScaleX = TableScale.ScaleY = Math.Max(scaleX, scaleY);
            }
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (canClose)
                return;

            var result = MessageBox.Show(this, "Přejete si ukončit směnu?", "Ukončení směny", MessageBoxButton.YesNo, MessageBoxImage.Question);

            e.Cancel = true;

            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitScale();
        }
    }
}
