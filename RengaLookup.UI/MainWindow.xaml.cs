using RengaLookup.UI.ViewModel;
using System.Windows;

namespace RengaLookup.UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(IViewModel viewModel)
		{
			InitializeComponent();
			DataContext = viewModel;
		}
	}
}
