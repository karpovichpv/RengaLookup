using Inspector.Domain.Helpers;
using Inspector.Model;
using Renga;

namespace Inspector.Domain
{
    internal class SelectedObjectsGetter
    {
        private readonly IApplication _app;

        public SelectedObjectsGetter()
        {
            _app = ApplicationSingleton.GetApp();
        }

        public List<IOutObject> GetSelected()
        {
            List<IOutObject> result = [];
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
