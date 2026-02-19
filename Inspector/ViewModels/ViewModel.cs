using CommunityToolkit.Mvvm.Input;
using Inspector.Domain;
using Inspector.Model;
using Inspector.ViewModels.Helpers;
using System.Collections.ObjectModel;

namespace Inspector.ViewModels
{
    public class ViewModel : ViewModelBase, IViewModel
    {
        private readonly SelectedObjectsGetter _selectedObjectsGetter;

        public ViewModel()
        {
            _selectedObjectsGetter = new SelectedObjectsGetter();
        }

        private RelayCommand? _snoopSelectedObject;
        public RelayCommand SnoopSelectedObject
        {
            get
            {
                return _snoopSelectedObject ??= new RelayCommand(() =>
                {
                    if (CurrentObject != null && CurrentObject.Object != null)
                    {
                        var infoCollector = new ChiefCollector(CurrentObject.Object);
                        Data = infoCollector.Collect().ToObservableCollection();
                    }
                },
                () => CurrentObject != null && CurrentObject.Object != null
                );
            }
        }

        private RelayCommand? _getSelectedObjects;
        public RelayCommand GetSelectedObjects
        {
            get
            {
                return _getSelectedObjects ??= new RelayCommand(() =>
                {
                    SelectedObjects = _selectedObjectsGetter.GetSelected().ToObservableCollection();
                    IOutObject? outObject = SelectedObjects.FirstOrDefault();
                    if (outObject != null)
                    {
                        CurrentObject = outObject;
                        var infoCollector = new ChiefCollector(outObject.Object);
                        Data = infoCollector.Collect().ToObservableCollection();
                    }
                });
            }
        }

        private RelayCommand? _runNewWindow;
        public RelayCommand? RunNewWindow
        {
            get
            {
                return _runNewWindow ??= new RelayCommand(
                        () =>
                        {
                            if (SelectedData != null)
                                new NewInstanceRunner(SelectedData).RunNewInstance();
                        },
                        () => SelectedData != null && SelectedData.CanGet)
;
            }
        }

        private ObservableCollection<IOutObject> _selectedObjects = [];
        public ObservableCollection<IOutObject> SelectedObjects
        {
            get
            {
                return _selectedObjects;
            }
            set
            {
                _selectedObjects = value;
                RaisePropertyChange(nameof(SelectedObjects));
            }
        }

        private IOutObject? _currentObject;
        public IOutObject? CurrentObject
        {
            get
            {
                return _currentObject;
            }
            set
            {
                _currentObject = value;
                RaisePropertyChange(nameof(CurrentObject));
            }
        }

        private IData? _selectedData;
        public IData? SelectedData
        {
            get
            {
                return _selectedData;
            }
            set
            {
                _selectedData = value;
                RaisePropertyChange(nameof(SelectedData));
            }
        }

        private ObservableCollection<IData>? _data;
        public ObservableCollection<IData>? Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChange(nameof(Data));
            }
        }
    }
}
