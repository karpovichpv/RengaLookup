using Renga;
using RengaLookup.Plugin.Domain.Model;
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
					PropertyInfo[] propertyInfos = @interface.GetProperties();

					if (@interface.IsInstanceOfType(_modelObject))
					{
						object? castedObject = Convert.ChangeType(_modelObject, @interface);
						IEnumerable<Data> dataSet = GetInfoFromProperties(castedObject, propertyInfos);

						builder.AppendLine(@interface.FullName);
					}
				}
			}

			return builder.ToString();
		}

		private IEnumerable<Data> GetInfoFromProperties(
			object? obj,
			PropertyInfo[] infos)
		{
			List<Data> result = [];
			foreach (PropertyInfo info in infos)
			{
				object? value = info.GetValue(obj);
				result.Add(new PropertyData() { Label = info.Name, Value = value });
			}

			return result;
		}
	}
}
