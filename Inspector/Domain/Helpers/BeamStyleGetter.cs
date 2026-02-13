using Renga;
using Inspector.Model;

namespace Inspector.Domain.Helpers
{
    internal static class BeamStyleGetter
    {
        public static OutObject Get(IBeamParams beamParams)
        {
            IApplication app = new Application();
            IProject project = app.Project;
            IBeamStyle style = project.BeamStyleManager.GetBeamStyle(beamParams.StyleId);

            return new OutObject(style, style.Name);
        }
    }
}
