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

        public static IModelObject? GetObject(int id)
        {
            Application? app = new();
            IModel? model = ModelGetter.GetModel(app);
            IModelObjectCollection? objects = model?.GetObjects();
            if (objects != null)
            {
                IModelObject? result = objects.GetById(id);
                return result;
            }

            return null;
        }
    }
}
