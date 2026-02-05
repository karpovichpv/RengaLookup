using System.IO;
using System.Reflection;

namespace RengaLookup.Plugin2.ViewModels.Helpers
{
    internal static class TitleGetter
    {
        private const string _name = "Renga Lookup 2";

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

                return $"{_name} v.{version} ({result})";
            }

            return string.Empty;
        }
    }
}