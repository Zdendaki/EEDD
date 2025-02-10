using System;
using System.Windows;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static byte[]? Token { get; set; }

        internal static DateTime? TokenGenerated { get; set; }

        internal static string? UserName { get; set; }

        internal static RuntimeData Data { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Data = new RuntimeData();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
