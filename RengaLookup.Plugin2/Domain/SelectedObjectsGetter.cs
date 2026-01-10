using Renga;
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

        public List<RengaObject> GetSelected()
        {
            List<RengaObject> result = [];
            if (_app is null)
                return result;

            IModel model = ModelGetter.GetModel(_app);
            if (model is null)
                return result;

            ISelection selection = _app.Selection;
            int[] array = (int[])selection.GetSelectedObjects();

            IModelObjectCollection modelObjects = model.GetObjects();
            foreach (int index in array)
            {
                IModelObject modelObject = modelObjects.GetById(index);
                if (modelObject != null)
                    result.Add(new RengaObject() { Name = modelObject.Name, Object = modelObject });
            }
            return result;
        }
    }
}
