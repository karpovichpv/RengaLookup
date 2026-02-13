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
            SelectedObjects = new ObservableCollection<IOutObject>(GetSampleSelectedObjects());
            Data = new ObservableCollection<IData>(GetSampleData());
        }

        public ObservableCollection<IOutObject> SelectedObjects { get; set; }

        public ObservableCollection<IData>? Data { get; set; }

        public RelayCommand SnoopSelectedObject => throw new NotImplementedException();

        public RelayCommand GetSelectedObjects => throw new NotImplementedException();

        public RelayCommand? RunNewWindow => throw new NotImplementedException();

        public IOutObject? CurrentObject { get; set; }

        public IData? SelectedData { get; set; }

        private static IEnumerable<IOutObject> GetSampleSelectedObjects()
        {
            return [
                            new DesignTimeOutObject()
                {
                    Name = "Колонна - Прямоугольная 400х400",
                },
                new DesignTimeOutObject()
                {
                    Name = "Балка - Прямоугольная 400х400",
                },
                new DesignTimeOutObject()
                {
                    Name = "Лестница: 16 шт x 250.00 мм x 125.00 мм"
                },
            ];
        }

        private static IEnumerable<IData> GetSampleData()
        {
            return [
                            new DesignTimeData()
                {
                    IsInterfaceHeader = true,
                    Label = "IModelObject",
                },
                new DesignTimeData()
                {
                    IsSubHeader = true,
                    Label = "Properties"
                },
                new DesignTimeData()
                {
                    Label = "ObjectType",
                    Value = "System.Guid",
                    CanGet = true,
                },
                new DesignTimeData()
                {
                    Label = "Id",
                    Value = "104688",
                },
                new DesignTimeData()
                {
                    Label = "Name",
                    Value = "Дверь - (нет): 900.00 мм x 2 140.00 мм"
                },
                new DesignTimeData()
                {
                    Label = "ObjectTypeS",
                    Value = "{1CFBA99C-01E7-4078-AE1A-3E2FF0673599}"
                },
                new DesignTimeData()
                {
                    Label = "UniqueId",
                    Value = "System.Guid",
                    CanGet = true
                },
                new DesignTimeData()
                {
                    Label = "FilePath",
                    Value = "C:/Program Files/Renga Standard/Samples/Apartment building.rnp",
                },
            ];
        }
    }
}
