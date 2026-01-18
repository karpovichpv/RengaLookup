using CommunityToolkit.Mvvm.Input;
using RengaLookup.Plugin2.Domain;
using RengaLookup.Plugin2.Model;
using RengaLookup.Plugin2.Model.Data;
using RengaLookup.Plugin2.ViewModels.Helpers;
using System.Collections.ObjectModel;

namespace RengaLookup.Plugin2.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private readonly SelectedObjectsGetter _selectedObjectsGetter;

        public ViewModel(ObservableCollection<RengaObject> selectedObjects = null)
        {
            _selectedObjects = DesignTimeInfoGetter.GetSelectedObjects();
            _selectedObjectsGetter = new SelectedObjectsGetter();
            if (selectedObjects != null)
                _selectedObjects = selectedObjects;
        }

        private RelayCommand _snoopSelectedObject;
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

        private RelayCommand _getSelectedObjects;
        public RelayCommand GetSelectedObjects
        {
            get
            {
                return _getSelectedObjects ??= new RelayCommand(() =>
                {
                    SelectedObjects = _selectedObjectsGetter.GetSelected().ToObservableCollection();
                });
            }
        }

        private RelayCommand _runNewWindow;
        public RelayCommand RunNewWindow
        {
            get
            {
                return _runNewWindow ??= new RelayCommand(
                        () => new NewInstanceRunner(SelectedData).RunNewInstance(),
                        () => SelectedData != null && SelectedData.CanGet)
;
            }
        }

        private ObservableCollection<RengaObject> _selectedObjects = [];
        public ObservableCollection<RengaObject> SelectedObjects
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

        private RengaObject _currentObject;
        public RengaObject CurrentObject
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

        private BaseData _selectedData;
        public BaseData SelectedData
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

        private ObservableCollection<BaseData> _data;
        public ObservableCollection<BaseData> Data
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
