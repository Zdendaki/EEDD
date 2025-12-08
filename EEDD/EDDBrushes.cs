using System.Windows.Media;

namespace EEDD
{
    static class EDDBrushes
    {
        public static Brush Transparent { get => new SolidColorBrush(Colors.Transparent); }

        public static Brush Black { get => new SolidColorBrush(Colors.Black); }

        public static Brush Red { get => new SolidColorBrush(Color.FromRgb(255, 0, 0)); }

        public static Brush Gray { get => new SolidColorBrush(Color.FromRgb(120, 120, 120)); }

        public static Brush Turqoise { get => new SolidColorBrush(Color.FromRgb(64, 224, 208)); }

        public static Brush LightBlue { get => new SolidColorBrush(Color.FromRgb(63, 72, 204)); }

        public static Brush Blue { get => new SolidColorBrush(Color.FromRgb(0, 0, 198)); }

        public static Brush DarkBlue { get => new SolidColorBrush(Color.FromRgb(23, 67, 163)); }

        public static Brush Brown { get => new SolidColorBrush(Color.FromRgb(136, 64, 16)); }

        public static Brush DarkGreen { get => new SolidColorBrush(Color.FromRgb(0, 119, 60)); }

        public static Brush DarkRed { get => new SolidColorBrush(Color.FromRgb(204, 0, 0)); }

        public static Brush BackgroundGray1 { get => new SolidColorBrush(Color.FromRgb(225, 225, 225)); }

        public static Brush BackgroundGray2 { get => new SolidColorBrush(Color.FromRgb(202, 202, 202)); }

        public static Brush BackgroundGreen1 { get => new SolidColorBrush(Color.FromRgb(180, 200, 200)); }

        public static Brush BackgroundGreen2 { get => new SolidColorBrush(Color.FromRgb(150, 180, 180)); }

        public static Brush BackgroundYellow1 { get => new SolidColorBrush(Color.FromRgb(250, 236, 160)); }

        public static Brush BackgroundYellow2 { get => new SolidColorBrush(Color.FromRgb(255, 242, 116)); }
    }
}
