using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal class EntityCollectionGetter
    {
        public static List<OutObject> Get(IEntityCollection collection)
        {
            List<OutObject> result = [];
            for (int i = 0; i < collection.Count; ++i)
            {
                var obj = collection.GetByIndex(i);
                if (obj != null)
                    result.Add(new OutObject(obj, obj.Name));
            }

            return result;
        }
    }
}
