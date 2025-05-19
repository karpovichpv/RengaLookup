using RengaLookup.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel;

namespace RengaLookup.UIControl.ViewModel
{
	public interface IViewModel : INotifyPropertyChanged
	{
		IEnumerable<IInterfaceInfo> InfoSet { get; set; }
	}
}
