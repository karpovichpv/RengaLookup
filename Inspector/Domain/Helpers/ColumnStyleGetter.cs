using Renga;
using Inspector.Model;

namespace Inspector.Domain.Helpers
{
    internal static class ColumnStyleGetter
    {
        public static OutObject Get(IColumnParams @params)
        {
            IApplication app = new Application();
            IProject project = app.Project;
            IColumnStyle style = project.ColumnStyleManager.GetColumnStyle(@params.StyleId);

            return new OutObject(style, style.Name);
        }
    }
}
