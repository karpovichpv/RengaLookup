using Renga;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal class MaterialGetter
    {
        public static IMaterial GetMaterial(int id)
        {
            IApplication app = new Application();
            IProject project = app.Project;
            IMaterialManager materialManager = project.MaterialManager;
            IMaterial material = materialManager.GetMaterial(id);

            return material;
        }
    }
}
