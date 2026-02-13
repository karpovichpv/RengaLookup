using CommunityToolkit.Mvvm.Input;
using Inspector.Model;
using System.Collections.ObjectModel;

namespace Inspector.ViewModels
{
    public interface IViewModel
    {
        RelayCommand SnoopSelectedObject { get; }
        RelayCommand GetSelectedObjects { get; }
        RelayCommand? RunNewWindow { get; }
        ObservableCollection<IOutObject> SelectedObjects { get; set; }
        IOutObject? CurrentObject { get; set; }
        IData? SelectedData { get; set; }
        ObservableCollection<IData>? Data { get; set; }
    }
}
