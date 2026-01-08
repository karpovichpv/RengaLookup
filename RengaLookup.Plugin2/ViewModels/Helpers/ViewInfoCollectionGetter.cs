using RengaLookup.Model.Contracts;
using RengaLookup.Plugin2.Model.Contracts;

namespace RengaLookup.Plugin2.ViewModels.Helpers
{
    internal static class ViewInfoCollectionGetter
    {
        public static List<ViewInfo> Get(IEnumerable<IInterfaceInfo> infoSet)
        {
            List<ViewInfo> result = [];
            foreach (IInterfaceInfo interfaceInfo in infoSet)
            {
                result.Add(new ViewInfo()
                {
                    IsMainHeader = true,
                    Name = interfaceInfo.Name,
                });

                IEnumerable<IInfo> properties = interfaceInfo.InfoSet
                    .Where(i => i.Type is SyntaxType.Property);
                result.AddRange(Convert(properties, "Properties"));

                IEnumerable<IInfo> methods = interfaceInfo.InfoSet
                    .Where(i => i.Type is SyntaxType.Method);
                result.AddRange(Convert(methods, "Methods"));

                IEnumerable<IInfo> fields = interfaceInfo.InfoSet
                    .Where(i => i.Type is SyntaxType.Field);
                result.AddRange(Convert(fields, "Fields"));
            }

            return result;
        }

        public static List<ViewInfo> Convert(IEnumerable<IInfo> value, string subHeaderTitle)
        {
            List<ViewInfo> result = [];
            if (value.Any())
            {
                result.Add(new ViewInfo()
                {
                    IsSubHeader = true,
                    Name = subHeaderTitle,
                });

                result.AddRange(value.Select(i => new ViewInfo()
                {
                    Name = i.Name,
                    Value = i.Value
                }));
            }

            return result;
        }
    }
}