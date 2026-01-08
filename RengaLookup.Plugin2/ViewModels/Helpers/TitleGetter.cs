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
            string shortName = assembly.GetName().Name;
            string resName = $"{shortName}.Resources.BuildDate.txt";

            string result;
            using (Stream stream = assembly.GetManifestResourceStream(resName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadLine().Replace("\r\n", string.Empty);
            }

            return $"{_name} v.{assembly.GetName().Version.ToString(4)} ({result})";
        }
    }
}