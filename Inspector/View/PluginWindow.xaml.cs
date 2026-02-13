using Inspector.ViewModels;
using System.Reflection;
using System.Windows;

namespace Inspector.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PluginWindow : Window
    {
        public PluginWindow()
        {
            InitializeComponent();
        }

        public PluginWindow(ViewModel viewModel)
        {
            DataContext = viewModel;
            Application.ResourceAssembly = Assembly.GetExecutingAssembly();
            InitializeComponent();
        }
    }
}
