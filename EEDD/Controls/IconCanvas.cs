using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EEDD.Controls
{
    public class IconCanvas : Decorator
    {
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(IconCanvas), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(Path), typeof(IconCanvas), new PropertyMetadata(null));

        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public Path Icon
        {
            get => (Path)GetValue(IconProperty);
            set 
            { 
                SetValue(IconProperty, value);
                Child = value;
            }
        }
    }
}
