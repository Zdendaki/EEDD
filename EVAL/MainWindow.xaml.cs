using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EVAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = $"EVAL [přihlášený uživatel {App.User?.Name}]";


        }

        private void Expander_Changed(object sender, RoutedEventArgs e)
        {
            for (Visual? vis = sender as Visual; vis is not null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow row)
                {
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
            }
        }
    }
}