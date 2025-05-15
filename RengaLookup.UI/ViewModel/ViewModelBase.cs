using RengaLookup.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel;

namespace RengaLookup.UI.ViewModel
{
	public abstract class ViewModelBase : IViewModel
	{

		public event PropertyChangedEventHandler PropertyChanged;
		public abstract IEnumerable<IInterfaceInfo> InfoSet { get; set; }

		private protected void RaisePropertyChange(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
