using Common.Data;
using System.Windows;
using System.Windows.Controls;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        private readonly Label[] _signallers;
        private readonly TextBlock[] _signallerNames;

        public Header()
        {
            InitializeComponent();

            _signallers = [Sig1L, Sig2L, Sig3L, Sig4L, Sig5L, Sig6L];
            _signallerNames = [Sig1, Sig2, Sig3, Sig4, Sig5, Sig6];
        }

        public void Init()
        {
            for (int i = 0; i < Client.MAX_SIGNALLERS; i++)
            {
                if (App.ClientData.Signallers.Count > i)
                {
                    _signallers[i].Visibility = Visibility.Visible;
                    _signallerNames[i].Text = GetSignallerName(App.ClientData.Signallers[i].Name);
                }
                else
                {
                    _signallers[i].Visibility = Visibility.Collapsed;
                }
            }
        }

        private string GetSignallerName(string name)
        {
            name = name.Trim();

            if (name.Length > 2)
                name = name.Substring(0, 2);

            if (name.Length > 1)
                name = name.Insert(1, Environment.NewLine);

            return name;
        }
    }
}
