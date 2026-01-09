using CommunityToolkit.Mvvm.Input;
using Renga;
using RengaLookup.Plugin2.Domain;
using RengaLookup.Plugin2.Model.Contracts;
using RengaLookup.Plugin2.Model.Data;
using RengaLookup.Plugin2.ViewModels.Helpers;
using System.Collections.ObjectModel;

namespace RengaLookup.Plugin2.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            _infoCollection = DesignTimeInfoGetter.GetInfoCollection();
        }

        public ViewModel(object currentObject, IEnumerable<IInterfaceInfo> infoSet)
        {
            CurrentObject = currentObject;
            _infoCollection = ViewInfoCollectionGetter.Get(infoSet);
        }

        public RelayCommand _snoopSelectedObject;
        public RelayCommand SnoopSelectedObject
        {
            get
            {
                return _snoopSelectedObject ??= new RelayCommand(() =>
                {
                    var infoCollector = new InfoCollector((IModelObject)CurrentObject);
                    InfoCollection = infoCollector.Get().Get();
                });
            }
        }

        private ObservableCollection<BaseInfo> _data;
        public ObservableCollection<BaseInfo> Data
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

        private IEnumerable<ViewInfo> _infoCollection;
        public override IEnumerable<ViewInfo> InfoCollection
        {
            get
            {
                return _infoCollection;
            }
            set
            {
                _infoCollection = value;
                RaisePropertyChange(nameof(InfoCollection));
            }
        }

        public override object CurrentObject { get; }
    }
}
