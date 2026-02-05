using Renga;
using RengaLookup.Plugin2.Domain.Helpers;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain
{
    internal class SelectedObjectsGetter
    {
        private readonly Application _app;

        public SelectedObjectsGetter()
        {
            _app = new Application();
        }

        public List<OutObject> GetSelected()
        {
            List<OutObject> result = [];
            if (_app is null)
                return result;

            IModel? model = ModelGetter.GetModel(_app);
            if (model is null)
                return result;

            ISelection selection = _app.Selection;
            int[] array = (int[])selection.GetSelectedObjects();

            IModelObjectCollection modelObjects = model.GetObjects();
            foreach (int index in array)
            {
                IModelObject modelObject = modelObjects.GetById(index);
                if (modelObject != null)
                    result.Add(new OutObject(modelObject, modelObject.Name));
            }
            return result;
        }
    }
}
