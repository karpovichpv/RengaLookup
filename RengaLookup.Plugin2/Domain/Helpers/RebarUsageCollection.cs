using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class RebarUsageCollection
    {
        public static List<OutObject> GetUsages(IRebarUsageCollection collection)
        {
            List<OutObject> result = [];
            for (int i = 0; i < collection.Count; ++i)
            {
                IRebarUsage usage = collection.Get(i);
                result.Add(new OutObject(usage, $"IRebarUsage. Id: {i}"));
            }

            return result;
        }
    }
}
