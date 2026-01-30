using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class ModelObjectGetter
    {
        public static List<OutObject> GetObjects(IModelObjectCollection collection)
        {
            List<OutObject> result = [];
            Array ids = collection.GetIds();
            for (int i = 0; i < ids.Length; ++i)
            {
                IModelObject modelObject = collection.GetByIndex(i);
                result.Add(new OutObject(modelObject, modelObject.Name));
            }

            return result;
        }
    }
}
