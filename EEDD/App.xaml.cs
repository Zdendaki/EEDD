using Communication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static EDDClient Client { get; set; }

        internal static byte[]? Token { get; set; }

        internal static DateTime? TokenGenerated { get; set; }

        internal static string? UserName { get; set; }

        internal static RuntimeData Data { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Data = new RuntimeData();
            Client = new EDDClient("127.0.0.1", 9180);

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Client.Disconnect();

            base.OnExit(e);
        }
    }
}
