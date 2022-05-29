using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        
        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(10000)
            {
                Enabled = true,
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
            InitStatusBar();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            InitStatusBar();
        }

        private void InitStatusBar()
        {
            IsConnected.Content = App.Client.IsConnected ? "Připojeno" : "Odpojeno";
            LoggedUser.Content = "Přihlášený uživatel: " + App.UserName;
        }
    }
}
