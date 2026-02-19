using Renga;

namespace Inspector.Domain.Helpers
{
    internal class MaterialGetter
    {
        public static IMaterial GetMaterial(int id)
        {
            IApplication app = ApplicationSingleton.GetApp();
            IProject project = app.Project;
            IMaterialManager materialManager = project.MaterialManager;
            IMaterial material = materialManager.GetMaterial(id);

            return material;
        }
    }
}
