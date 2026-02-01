using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
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

        private static object GetStyleInternal(object obj)
        {
            IApplication app = new Application();
            IProject project = app.Project;
            return obj switch
            {
                IBeamParams b => project.BeamStyleManager.GetBeamStyle(b.StyleId),
                IColumnParams c => project.ColumnStyleManager.GetColumnStyle(c.StyleId),
                IDoorParams d => null,
                IFloorParams f => null,
                ILevelViewParams l => null,
                ILine3DParams l => null,
                IOpeningParams o => null,
                IPortDuctParams o => null,
                IPortPipeParams o => null,
                IRouteParams o => null,
                IView3DParams o => null,
                IWallParams o => null,
                IWindowParams o => null,



            };
        }
    }
}
