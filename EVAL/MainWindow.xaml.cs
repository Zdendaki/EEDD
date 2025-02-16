using System.Windows;

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

            Title = $"EVAL [přihlášený uživatel {App.User.Name}]";
        }
    }
}