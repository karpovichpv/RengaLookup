using Inspector.Model;
using Renga;

namespace Inspector.Domain.Helpers
{
    internal static class ColumnStyleGetter
    {
        public static OutObject Get(IColumnParams @params)
        {
            IApplication app = ApplicationSingleton.GetApp();
            IProject project = app.Project;
            IColumnStyle style = project.ColumnStyleManager.GetColumnStyle(@params.StyleId);

            return new OutObject(style, style.Name);
        }
    }
}
