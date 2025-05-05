using Renga;
using RengaLookup.Plugin.Domain.Model;
using System.Reflection;

namespace RengaLookup.Plugin.Domain
{
	internal class RengaInfoGetter(IModelObject modelObject)
	{
		private readonly IModelObject _modelObject
			= modelObject ?? throw new ArgumentNullException(nameof(modelObject));

		public IEnumerable<InterfaceEntry> Get()
		{
			List<InterfaceEntry> interfaceEntries = [];
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			AssemblyName[] referencedAssemblies = executingAssembly.GetReferencedAssemblies();
			List<AssemblyName> interopAssemblies = referencedAssemblies
				.Where(a => a.FullName.Contains("Interop"))
				.ToList();

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
					if (@interface.IsInstanceOfType(_modelObject))
					{
						PropertyInfo[] propertyInfos = @interface.GetProperties();
						IEnumerable<Data> propretiesDataSet = GetInfoFromProperties(_modelObject, propertyInfos);
						FieldInfo[] fieldInfos = @interface.GetFields();
						IEnumerable<Data> fieldsDataSet = GetInfoFromFields(_modelObject, fieldInfos);

						IEnumerable<Data> value = [.. propretiesDataSet, .. fieldsDataSet];
						InterfaceEntry interfaceEntry = new()
						{
							Name = @interface.Name,
							Infos = value
						};
						interfaceEntries.Add(interfaceEntry);
					}
				}
			}

			return interfaceEntries;
		}

		private static List<Data> GetInfoFromFields(object obj, FieldInfo[] infos)
		{
			List<Data> result = [];
			foreach (FieldInfo info in infos)
			{
				object? value = info.GetValue(obj);
				result.Add(new FieldData() { Label = info.Name, Value = value });
			}

			return result;
		}

		private static List<Data> GetInfoFromProperties(
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
