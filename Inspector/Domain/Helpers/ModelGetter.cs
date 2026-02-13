using Renga;

namespace Inspector.Domain.Helpers
{
    internal static class ModelGetter
    {
        public static IModel? GetModel(IApplication? app)
        {

            if (app?.ActiveView.Type is ViewType.ViewType_View3D or ViewType.ViewType_Level)
                return app.Project.Model;
            else if (app?.ActiveView.Type is ViewType.ViewType_Assembly)
            {
                IModelView? modelView = (app.ActiveView as IModelView);
                if (modelView != null)
                {
                    var representedId = modelView.RepresentedEntityId;
                    IEntityCollection assemblies = app.Project.Assemblies;
                    if (assemblies != null)
                    {
                        if (assemblies.GetById(representedId) is IModel model)
                            return model;
                    }
                }
            }
            else if (app?.ActiveView.Type is ViewType.ViewType_Drawing)
            {
                IModelView? modelView = (app.ActiveView as IModelView);
                if (modelView != null)
                {
                    var representedId = modelView.RepresentedEntityId;
                    IEntityCollection drawings = app.Project.Drawings2;
                    if (drawings != null)
                    {
                        if (drawings.GetById(representedId) is IModel model)
                            return model;
                    }
                }
            }

            return null;
        }
    }
}