using Common.Data;
using System.Windows;
using System.Windows.Controls;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro Row.xaml
    /// </summary>
    public partial class RowTrain : Row
    {
        private static readonly GridLength SIGNALLER_SHOWN = new(15);
        private static readonly GridLength SIGNALLER_HIDDEN = new(0);

        private readonly ColumnDefinition[] _signallers;

        StationColor _color;
        bool _chained;

        public RowTrain(StationColor color, bool chained) : base()
        {
            InitializeComponent();

            _signallers = [Signaller1, Signaller2, Signaller3, Signaller4, Signaller5, Signaller6];

            _color = color;
            _chained = chained;

            if (_chained)
            {
                border.BorderThickness = new(0.5d, 0.5d, 0.5d, 0);
                Margin = new(0, 0, 0, -0.5d);
            }
        }

        [Obsolete("Only for designer", true)]
        public RowTrain() : this(StationColor.Gray, false) { }

        public override bool SetBackground(bool odd)
        {
            Background = RowHelper.GetBackground(_color, odd);
            return _chained;
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
