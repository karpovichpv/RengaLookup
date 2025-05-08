namespace RengaLookup.UI.ViewModel
{
	public class ViewModel : ViewModelBase
	{

		private string _someText = "Some Text";
		public string SomeText
		{
			get => _someText;
			set
			{
				_someText = value;
				RaisePropertyChange(nameof(SomeText));
			}
		}
	}
}
