using Communication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static WSClient Client { get; set; }

        internal static byte[]? Token { get; set; }

        internal static DateTime? TokenGenerated { get; set; }

        internal static string? UserName { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Client = new WSClient(ClientType.Manager);
            
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Client.Disconnect();
            
            base.OnExit(e);
        }
    }
}
