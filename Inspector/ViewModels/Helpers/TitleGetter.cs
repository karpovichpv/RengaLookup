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
            string resName = $"{shortName}.Resources.BuildDate.txt";

            string? result = string.Empty;
            using (Stream? stream = assembly.GetManifestResourceStream(resName))
            {
                if (stream != null)
                {
                    using StreamReader reader = new(stream);
                    {
                        string? line = reader.ReadLine();
                        result = line?.Replace("\r\n", string.Empty);
                    }
                }
            }

            if (result != null)
            {
                AssemblyName assemblyName = assembly.GetName();
                string? version = string.Empty;
                if (assemblyName != null)
                    version = assemblyName.Version?.ToString(4);

                return $"{PluginConstants.PluginName} v.{version} ({result})";
            }

            return string.Empty;
        }
    }
}