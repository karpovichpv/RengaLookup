using RengaLookup.Plugin2.Model.Contracts;
using System.ComponentModel;

namespace RengaLookup.Plugin2.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
        IEnumerable<IInterfaceInfo> InfoSet { get; set; }
    }
}
