using Common.Data;
using EVAL.Endpoint;
using System.Windows;

namespace EVAL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly uint DeviceId = DeviceIdHelper.GetDeviceId();

        internal static EvalClient Client { get; set; } = null!;

        internal static User User { get; set; } = null!;
    }

}
