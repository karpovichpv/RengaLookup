using RengaLookup.Plugin2.ViewModels.Helpers;
using System.ComponentModel;

namespace RengaLookup.Plugin2.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public abstract IEnumerable<ViewInfo> InfoCollection { get; set; }
        public abstract object CurrentObject { get; }
        public string Title => TitleGetter.GetTitle(GetType());

        private protected void RaisePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
