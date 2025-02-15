using System.Windows;
using System.Windows.Media;

namespace EEDD
{
    static class Extensions
    {
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child is null)
                    continue;

                if (child is T tChild)
                    yield return tChild;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public static string Limit(this string value, int length)
        {
            if (value is null || length < 1)
                return string.Empty;

            return value.Substring(0, Math.Min(value.Length, length));
        }

        public static string Truncate(this string value, int length)
        {
            return (value.Length > length) ? value.Substring(0, length) : value.PadRight(2);
        }
    }
}
