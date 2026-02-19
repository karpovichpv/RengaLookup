using System.IO;
using System.Reflection;

namespace Inspector.ViewModels.Helpers
{
    internal static class TitleGetter
    {
        public static string GetTitle(Type type)
        {
            Assembly assembly = type.Assembly;
            string? shortName = assembly.GetName().Name;
            string? buildDate = GetBuildDate(assembly, shortName);

            if (buildDate != null)
            {
                AssemblyName assemblyName = assembly.GetName();
                string? version = string.Empty;
                if (assemblyName != null)
                    version = assemblyName.Version?.ToString(4);

                return $"{PluginConstants.PluginName} v.{version}-beta ({buildDate})";
            }

            return string.Empty;
        }

        private static string? GetBuildDate(Assembly assembly, string? shortName)
        {
            string resName = $"{shortName}.Resources.BuildDate.txt";

            string? buildDate = string.Empty;
            using (Stream? stream = assembly.GetManifestResourceStream(resName))
            {
                if (stream != null)
                {
                    using StreamReader reader = new(stream);
                    {
                        string? line = reader.ReadLine();
                        buildDate = line?.Replace("\r\n", string.Empty);
                    }
                }
            }

            return buildDate;
        }
    }
}