using Renga;
using System.Reflection;
using System.Text;

namespace RengaLookup.Plugin.Domain
{
	internal class RengaInfoGetter(IModelObject modelObject)
	{
		private readonly IModelObject _modelObject
			= modelObject ?? throw new ArgumentNullException(nameof(modelObject));

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
				IEnumerable<Type> interfaces = assembly
					.GetTypes()
					.Where(t => t.IsInterface);
				foreach (Type? @interface in interfaces)
				{
					// Try to cast using reflection
					if (@interface.IsInstanceOfType(_modelObject))
					{
						builder.AppendLine(@interface.FullName);
					}
				}
			}

			return builder.ToString();
		}
	}
}
