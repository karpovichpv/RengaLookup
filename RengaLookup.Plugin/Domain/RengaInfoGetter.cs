using System.Reflection;
using System.Text;

namespace RengaLookup.Plugin.Domain
{
	internal class RengaInfoGetter
	{
		public RengaInfoGetter()
		{
		}

		public string Get()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			var referencedAssemblies = executingAssembly.GetReferencedAssemblies();

			StringBuilder builder = new();
			foreach (AssemblyName assemblyName in referencedAssemblies)
			{
				builder.AppendLine($"Ass: {assemblyName.FullName}");
			}

			return builder.ToString();
		}
	}
}
