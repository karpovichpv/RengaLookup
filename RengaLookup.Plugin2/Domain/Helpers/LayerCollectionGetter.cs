using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class LayerCollectionGetter
    {
        public static List<OutObject> GetLayers(ILayerCollection layerCollection)
        {
            var result = new List<OutObject>();
            for (int i = 0; i < layerCollection.Count; i++)
            {
                ILayer layer = layerCollection.Get(i);
                result.Add(new OutObject(layer, $"Layer. MaterialId {layer.MaterialId}"));
            }

            return result;
        }
    }
}
