using Renga;

namespace RengaLookup.Plugin2.Domain
{
    internal static class ModelGetter
    {
        public static IModel GetModel(IApplication app)
        {
            if (app.ActiveView.Type is ViewType.ViewType_View3D or ViewType.ViewType_Level)
                return app.Project.Model;
            else if (app.ActiveView.Type is ViewType.ViewType_Assembly)
            {
                var representedId = (app.ActiveView as IModelView).RepresentedEntityId;
                return app.Project.Assemblies.GetById(representedId) as IModel;
            }
            else if (app.ActiveView.Type is ViewType.ViewType_Drawing)
            {
                var representedId = (app.ActiveView as IModelView).RepresentedEntityId;
                return app.Project.Drawings2.GetById(representedId) as IModel;
            }

            return null;
        }
    }
}