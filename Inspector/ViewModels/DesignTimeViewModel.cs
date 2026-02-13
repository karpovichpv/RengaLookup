using CommunityToolkit.Mvvm.Input;
using Inspector.Model;
using Inspector.ViewModels.Data;
using System.Collections.ObjectModel;

namespace Inspector.ViewModels
{
    public class DesignTimeViewModel : IViewModel
    {
        public DesignTimeViewModel()
        {
            IEnumerable<IOutObject> selectedObjects =
            [
                new DesignTimeOutObject(),
                new DesignTimeOutObject(),
                new DesignTimeOutObject(),
            ];
            SelectedObjects = new ObservableCollection<IOutObject>(selectedObjects);
            IEnumerable<IData> data =
            [
                new DesignTimeData(),
                new DesignTimeData(),
                new DesignTimeData(),
                new DesignTimeData(),
                new DesignTimeData(),
                new DesignTimeData(),
                new DesignTimeData(),
            ];
            Data = new ObservableCollection<IData>(data);
        }

        public ObservableCollection<IOutObject> SelectedObjects { get; set; }

        public ObservableCollection<IData>? Data { get; set; }

        public RelayCommand SnoopSelectedObject => throw new NotImplementedException();

        public RelayCommand GetSelectedObjects => throw new NotImplementedException();

        public RelayCommand? RunNewWindow => throw new NotImplementedException();

        public IOutObject? CurrentObject { get; set; }

        public IData? SelectedData { get; set; }
    }
}
