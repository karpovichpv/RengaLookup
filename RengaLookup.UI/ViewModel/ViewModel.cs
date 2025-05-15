using RengaLookup.Model.Contracts;
using System.Collections.Generic;

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

		private IEnumerable<IInterfaceInfo> _infoSet;
		public override IEnumerable<IInterfaceInfo> InfoSet
		{
			get => _infoSet;
			set
			{
				_infoSet = value;
				RaisePropertyChange(nameof(InfoSet));
			}
		}
	}
}
