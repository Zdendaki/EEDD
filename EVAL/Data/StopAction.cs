using Common.Data;
using System.Windows;

namespace EVAL.Data
{
    public class StopAction : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name), typeof(string), typeof(StopAction), new PropertyMetadata(""));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(StopAction), new PropertyMetadata(false));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public StopAction() { }

        public StopAction(TrainAction action)
        {
            Name = action.Name;
        }
    }
}
