using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inspector.View.Behaviours
{
    public static class ListViewItemBehaviors
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand",
                typeof(ICommand), typeof(ListViewItemBehaviors),
                new PropertyMetadata(null, OnChanged));

        public static void SetDoubleClickCommand(UIElement element, ICommand value)
            => element.SetValue(DoubleClickCommandProperty, value);

        public static ICommand GetDoubleClickCommand(UIElement element)
            => (ICommand)element.GetValue(DoubleClickCommandProperty);

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListView control)
            {
                control.MouseDoubleClick += (s, args) =>
                {
                    var command = GetDoubleClickCommand(control);
                    if (command?.CanExecute(control.SelectedItem) == true)
                        command.Execute(control.SelectedItem);
                };
            }
        }
    }

}
