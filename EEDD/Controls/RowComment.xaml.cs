using Common.Data;
using System.Windows;
using System.Windows.Controls;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro Row.xaml
    /// </summary>
    public partial class RowComment : Row
    {
        private static readonly GridLength SIGNALLER_SHOWN = new(15);
        private static readonly GridLength SIGNALLER_HIDDEN = new(0);

        private readonly ColumnDefinition[] _signallers;

        public RowComment() : base()
        {
            InitializeComponent();

            _signallers = [Signaller1, Signaller2, Signaller3, Signaller4, Signaller5, Signaller6];
        }

        protected override void Init()
        {
            for (int i = 0; i < Client.MAX_SIGNALLERS; i++)
            {
                if (App.ClientData.Signallers.Count > i)
                {
                    _signallers[i].Width = SIGNALLER_SHOWN;
                }
                else
                {
                    _signallers[i].Width = SIGNALLER_HIDDEN;
                }
            }
        }
    }
}
