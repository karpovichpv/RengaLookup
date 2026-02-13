using Inspector.Domain;
using Inspector.Helpers;
using Inspector.Model;
using Inspector.View;
using Inspector.ViewModels.Helpers;

namespace Inspector.ViewModels
{
    internal class NewInstanceRunner
    {
        private readonly IData _selectedData;

        public NewInstanceRunner(IData selectedData)
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
            IEnumerable<IOutObject> objects = _selectedData.WalkDown();
            IOutObject? outObject = objects.FirstOrDefault();
            IOutObject? modelObject = objects.FirstOrDefault();
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
