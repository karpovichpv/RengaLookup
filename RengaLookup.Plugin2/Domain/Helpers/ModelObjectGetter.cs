using Renga;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class ModelObjectGetter
    {
        public static List<object> GetObjects(IModelObjectCollection collection)
        {
            List<object> result = [];
            Array ids = collection.GetIds();
            for (int i = 0; i < ids.Length; ++i)
            {
                IModelObject modelObject = collection.GetByIndex(i);
                result.Add(modelObject);
            }

            return result;
        }
    }
}
