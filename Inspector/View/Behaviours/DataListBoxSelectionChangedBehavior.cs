using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inspector.View.Behaviours
{
    public static class DataListBoxDoubleClickBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(DataListBoxDoubleClickBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static void SetCommand(DependencyObject obj, ICommand value) =>
            obj.SetValue(CommandProperty, value);

        public static ICommand GetCommand(DependencyObject obj) =>
            (ICommand)obj.GetValue(CommandProperty);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                // Remove old handler if any
                listBox.MouseDoubleClick -= ListBox_MouseDoubleClick;

                if (e.NewValue is ICommand)
                {
                    listBox.MouseDoubleClick += ListBox_MouseDoubleClick;
                }
            }
        }

        private static void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var command = GetCommand(listBox);
                var selectedItem = listBox.SelectedItem;

                if (selectedItem != null && command?.CanExecute(selectedItem) == true)
                {
                    command.Execute(selectedItem);
                }
            }
        }
    }


}
