using Renga;
using RengaLookup.Plugin.Domain.Model;
using RengaLookup.Plugin.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RengaLookup.Plugin.Domain
{
	internal class RengaInfoGetter
	{
		private readonly IModelObject _modelObject;

		public RengaInfoGetter(IModelObject modelObject)
		{
			_modelObject = modelObject ?? throw new ArgumentNullException(nameof(modelObject));
		}

		public IEnumerable<InterfaceEntry> Get()
		{
			var interfaceEntries = new List<InterfaceEntry>();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			AssemblyName[] referencedAssemblies = executingAssembly.GetReferencedAssemblies();
			List<AssemblyName> interopAssemblies = referencedAssemblies
				.Where(a => a.FullName.Contains("Interop"))
				.ToList();

			if (interopAssemblies != null)
			{
				AssemblyName interopAssembly = interopAssemblies[0];
				Assembly assembly = Assembly.Load(interopAssembly);

				// Get all interfaces
				IEnumerable<Type> interfaces = assembly
					.GetTypes()
					.Where(t => t.IsInterface);
				foreach (Type @interface in interfaces)
				{
					if (@interface.IsInstanceOfType(_modelObject))
					{
						PropertyInfo[] propertyInfos = @interface.GetProperties();
						IEnumerable<Data> propretiesDataSet = GetInfoFromProperties(_modelObject, propertyInfos);
						FieldInfo[] fieldInfos = @interface.GetFields();
						IEnumerable<Data> fieldsDataSet = GetInfoFromFields(_modelObject, fieldInfos);

						var value = new List<Data>();
						value.AddRange(propretiesDataSet);
						value.AddRange(fieldsDataSet);
						InterfaceEntry interfaceEntry = new InterfaceEntry()
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
			var result = new List<Data>();
			foreach (FieldInfo info in infos)
			{
				object value = info.GetValue(obj);
				result.Add(new FieldData() { Label = info.Name, Value = value });
			}

			return result;
		}

		private static List<Data> GetInfoFromProperties(
			object obj,
			PropertyInfo[] infos)
		{
			var result = new List<Data>();
			foreach (PropertyInfo info in infos)
			{
				object value = info.GetValue(obj);
				result.Add(new PropertyData() { Label = info.Name, Value = value });
			}

			return result;
		}
	}
}
