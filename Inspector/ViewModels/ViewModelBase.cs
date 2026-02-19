using Inspector.ViewModels.Helpers;
using System.ComponentModel;

namespace Inspector.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public string Title => TitleGetter.GetTitle(GetType());

        private protected void RaisePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
