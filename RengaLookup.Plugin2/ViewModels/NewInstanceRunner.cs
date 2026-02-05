using RengaLookup.Plugin2.Domain;
using RengaLookup.Plugin2.Helpers;
using RengaLookup.Plugin2.Model;
using RengaLookup.Plugin2.Model.Data;
using RengaLookup.Plugin2.View;
using RengaLookup.Plugin2.ViewModels.Helpers;

namespace RengaLookup.Plugin2.ViewModels
{
    internal class NewInstanceRunner
    {
        private readonly BaseData _selectedData;

        public NewInstanceRunner(BaseData selectedData)
        {
            _selectedData = selectedData ?? throw new ArgumentNullException(nameof(selectedData));
        }

        public void RunNewInstance()
        {
            ViewModel? subViewModel = GetViewModel();

            var window = new PluginWindow
            {
                DataContext = subViewModel
            };
            window.Show();
        }

        private ViewModel? GetViewModel()
        {
            IEnumerable<OutObject> objects = _selectedData.WalkDown();
            OutObject? outObject = objects.FirstOrDefault();
            OutObject? modelObject = objects.FirstOrDefault();
            if (outObject != null && modelObject != null)
            {
                ViewModel subViewModel = new()
                {
                    CurrentObject = outObject.ToRengaObject(),
                    SelectedObjects = objects.ToObservableCollection(),
                    Data = new ChiefCollector(modelObject)
                        .Collect()
                        .ToObservableCollection()
                };
                return subViewModel;
            }

            return null;
        }
    }
}
