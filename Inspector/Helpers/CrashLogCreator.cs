using System.IO;
using System.Reflection;

namespace Inspector.Helpers
{
    internal static class CrashLogCreator
    {
        public static void WriteCrashLog(Exception ex, Type type)
        {
            Assembly? assembly = Assembly.GetAssembly(type);
            string? location = assembly?.Location;
            string? currentFolder = Path.GetDirectoryName(location);
            string? shortName = assembly?.GetName().Name;
            if (shortName != null && currentFolder != null)
            {
                string? logPath = Path.Combine(currentFolder, $"{shortName}_crash.log");
                if (File.Exists(logPath))
                    File.Delete(logPath);

                File.WriteAllText(logPath, ex.ToString());
            }
        }
    }
}