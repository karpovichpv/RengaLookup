using CommunityToolkit.Mvvm.Input;
using Inspector.Model;
using Inspector.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inspector.View.UserControls
{
    /// <summary>
    /// Interaction logic for SelectedObjectsControl.xaml
    /// </summary>
    public partial class DataListBoxControl : UserControl
    {
        public DataListBoxControl()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is IData data)
            {
                IViewModel? viewModel = (DataContext as IViewModel);
                if (viewModel != null)
                {
                    RelayCommand? command = viewModel.RunNewWindow;
                    command?.Execute(null);
                }
            }
        }
    }
}
