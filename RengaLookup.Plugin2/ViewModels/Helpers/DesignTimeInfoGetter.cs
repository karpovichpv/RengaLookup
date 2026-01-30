using RengaLookup.Plugin2.Model;
using System.Collections.ObjectModel;

namespace RengaLookup.Plugin2.ViewModels.Helpers
{
    internal static class DesignTimeInfoGetter
    {
        public static IEnumerable<ViewInfo> GetInfoCollection()
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

        internal static ObservableCollection<OutObject> GetSelectedObjects()
        {
            List<OutObject> objects =
            [
                new OutObject(null,"Beam 1"),
                new OutObject(null,"Beam 2"),
                new OutObject(null,"Beam 3"),
            ];

            return new ObservableCollection<OutObject>(objects);
        }
    }
}