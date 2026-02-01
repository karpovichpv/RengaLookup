using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class BeamStyleGetter
    {
        public static OutObject GetStyle(int id)
        {
            IApplication application = new Renga.Application();
            IBeamStyleManager manager = application.Project.BeamStyleManager;

            IBeamStyle style = manager.GetBeamStyle(id);
            return new OutObject(style, style.Name);
        }
    }
}
