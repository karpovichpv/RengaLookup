using RengaLookup.Model.Contracts;
using RengaLookup.Model.Implementations;
using RengaLookup.Plugin2.Model.Contracts;
using RengaLookup.Plugin2.ViewModels.Helpers;

namespace RengaLookup.Plugin2.ViewModels
{
    public class DesignViewModel : ViewModelBase
    {
        public DesignViewModel()
        {
            _infoSet = GetInfoSet();
            _infoCollection = GetInfoCollection();
        }

        public DesignViewModel(IEnumerable<IInterfaceInfo> infoSet = null)
        {
            if (infoSet is null)
            {
                _infoSet = GetInfoSet();
                _infoCollection = GetInfoCollection();
            }
            else
            {
                _infoSet = infoSet;
                _infoCollection = ViewInfoCollectionGetter.Get(infoSet);
            }
        }

        private static IEnumerable<ViewInfo> GetInfoCollection()
        {
            return
            [
                new()
                {
                    IsMainHeader = true,
                    Name = "Interface1",
                },
                new()
                {
                   IsSubHeader = true,
                   Name = "Properties",
                },
                new()
                {
                   Name = "Name 1",
                   Value = "50.0",
                },
                new()
                {
                   Name = "Name 1",
                   Value = "50.0",
                }


            ];
        }

        private IEnumerable<IInterfaceInfo> _infoSet;
        public override IEnumerable<IInterfaceInfo> InfoSet
        {
            get
            {
                return _infoSet;
            }
            set
            {
                _infoSet = value;
                RaisePropertyChange(nameof(InfoSet));
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
                RaisePropertyChange(nameof(InfoSet));
            }
        }

        private IEnumerable<IInterfaceInfo> GetInfoSet()
        {
            return new List<IInterfaceInfo>
            {
                GetInterfaceInfo("1"),
                GetInterfaceInfo("2"),
            };
        }

        private static IInterfaceInfo GetInterfaceInfo(string n)
        {
            return new InterfaceInfo()
            {
                Name = $"IInterface name {n}",
                InfoSet = new List<IInfo>()
                    {
                        new Info()
                        {
                            Name = $"Some name {1}",
                            Type = SyntaxType.Property,
                            Value = $"Value {1}"
                        },
                        new Info()
                        {
                            Name = $"Some name {2}",
                            Type = SyntaxType.Property,
                            Value = $"Value {2}"
                        }
                    }
            };
        }
    }
}
