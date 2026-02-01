using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal class Placement3dGetter
    {
        public static List<OutObject> Get(IPlacement3DCollection collection)
        {
            List<OutObject> result = [];
            for (int i = 0; i < collection.Count; ++i)
            {
                var placement = collection.Get(i);
                result.Add(new OutObject(placement, $"Placement. Id: {i}"));
            }

            return result;
        }
    }
}
