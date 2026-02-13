using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inspector.View.Behaviours
{
    public static class ElementsListBoxSelectionChangedBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand),
                typeof(ElementsListBoxSelectionChangedBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static void SetCommand(DependencyObject obj, ICommand value) =>
            obj.SetValue(CommandProperty, value);

        public static ICommand GetCommand(DependencyObject obj) =>
            (ICommand)obj.GetValue(CommandProperty);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                listBox.SelectionChanged += (s, args) =>
                {
                    var command = GetCommand(listBox);
                    if (command?.CanExecute(listBox.SelectedItem) == true)
                        command.Execute(listBox.SelectedItem);
                };
            }
        }
    }

}
