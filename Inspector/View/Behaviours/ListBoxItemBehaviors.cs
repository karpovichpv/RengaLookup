using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inspector.View.Behaviours
{
    public static class ListBoxItemBehaviors
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand",
                typeof(ICommand), typeof(ListBoxItemBehaviors),
                new PropertyMetadata(null, OnChanged));

        public static void SetDoubleClickCommand(UIElement element, ICommand value)
            => element.SetValue(DoubleClickCommandProperty, value);

        public static ICommand GetDoubleClickCommand(UIElement element)
            => (ICommand)element.GetValue(DoubleClickCommandProperty);

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                listBox.MouseDoubleClick += (s, args) =>
                {
                    var command = GetDoubleClickCommand(listBox);
                    if (command?.CanExecute(listBox.SelectedItem) == true)
                        command.Execute(listBox.SelectedItem);
                };
            }
        }
    }

}
