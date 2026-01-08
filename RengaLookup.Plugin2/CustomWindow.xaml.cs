using System.Windows;

namespace Net8PluginWithWPF
{
  public partial class CustomWindow : Window
  {
    public CustomWindow()
    {
      InitializeComponent();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
