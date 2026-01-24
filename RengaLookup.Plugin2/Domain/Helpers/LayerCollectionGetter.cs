using Renga;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class LayerCollectionGetter
    {
        public static List<object> GetLayers(ILayerCollection layerCollection)
        {
            var result = new List<object>();
            for (int i = 0; i < layerCollection.Count; i++)
            {
                ILayer layer = layerCollection.Get(i);
                result.Add(layer);
            }

            return result;
        }
    }
}
