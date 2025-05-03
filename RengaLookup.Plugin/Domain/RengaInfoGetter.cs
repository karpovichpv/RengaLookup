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
			AssemblyName[] referencedAssemblies = executingAssembly.GetReferencedAssemblies();
			List<AssemblyName> interopAssemblies = referencedAssemblies
				.Where(a => a.FullName.Contains("Interop"))
				.ToList();


			StringBuilder builder = new();
			if (interopAssemblies is not null)
			{
				AssemblyName interopAssembly = interopAssemblies[0];
				Assembly assembly = Assembly.Load(interopAssembly);

				// Get all interfaces
				var interfaces = assembly.GetTypes().Where(t => t.IsInterface);
				foreach (Type? iface in interfaces)
				{
					builder.AppendLine(iface.FullName);
				}
			}

			return builder.ToString();
		}
	}
}
