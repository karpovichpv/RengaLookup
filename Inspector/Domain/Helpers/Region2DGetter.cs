using Renga;
using Inspector.Model;

namespace Inspector.Domain.Helpers
{
    internal static class Region2DGetter
    {
        public static List<OutObject> Get(IRegion2DCollection collection)
        {
            List<OutObject> result = [];
            for (int i = 0; i < collection.Count; ++i)
            {
                IRegion2D region = collection.Get(i);
                result.Add(new OutObject(region, $"IRegion2D. Id: {i}"));
            }

            return result;
        }
    }
}
