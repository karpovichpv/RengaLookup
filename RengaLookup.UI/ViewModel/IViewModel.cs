using RengaLookup.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace RengaLookup.UI.ViewModel
{
	public interface IViewModel : INotifyPropertyChanged
	{
		IEnumerable<IInterfaceInfo> InfoSet { get; set; }
	}
}
