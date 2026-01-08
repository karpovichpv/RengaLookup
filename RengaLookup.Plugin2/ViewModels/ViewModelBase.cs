using RengaLookup.Plugin2.Model.Contracts;
using RengaLookup.Plugin2.ViewModels.Helpers;
using System.ComponentModel;

namespace RengaLookup.Plugin2.ViewModels
{
    public abstract class ViewModelBase : IViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public abstract IEnumerable<IInterfaceInfo> InfoSet { get; set; }
        public abstract IEnumerable<ViewInfo> InfoCollection { get; set; }
        public string Title => TitleGetter.GetTitle(GetType());

        private protected void RaisePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
