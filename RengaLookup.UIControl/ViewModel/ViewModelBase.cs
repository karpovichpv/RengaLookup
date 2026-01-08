using RengaLookup.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel;

namespace RengaLookup.UIControl.ViewModel
{
    public abstract class ViewModelBase : IViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public abstract IEnumerable<IInterfaceInfo> InfoSet { get; set; }
        public string Title => TitleGetter.GetTitle(GetType());

        private protected void RaisePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
