using System;
using System.IO;
using System.Reflection;

namespace RengaLookup.UIControl.ViewModel
{
    internal static class TitleGetter
    {
        private const string _name = "Renga Lookup";

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