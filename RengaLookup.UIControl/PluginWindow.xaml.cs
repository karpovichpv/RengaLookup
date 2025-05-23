using RengaLookup.UIControl.ViewModel;
using System.Reflection;
using System.Windows;

namespace RengaLookup.UIControl
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

		public PluginWindow(IViewModel viewModel)
		{
			DataContext = viewModel;
			Application.ResourceAssembly = Assembly.GetExecutingAssembly();
			InitializeComponent();
		}
	}
}
