using Renga;
using RengaLookup.Model.Contracts;
using RengaLookup.Model.Implementations;
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

		public IEnumerable<IInterfaceInfo> Get()
		{
			var interfaceEntries = new List<IInterfaceInfo>();
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
						IEnumerable<IInfo> propretiesDataSet = GetInfoFromProperties(_modelObject, propertyInfos);
						FieldInfo[] fieldInfos = @interface.GetFields();
						IEnumerable<IInfo> fieldsDataSet = GetInfoFromFields(_modelObject, fieldInfos);

						var value = new List<IInfo>();
						value.AddRange(propretiesDataSet);
						value.AddRange(fieldsDataSet);
						IInterfaceInfo interfaceEntry = new InterfaceInfo()
						{
							Name = @interface.Name,
							InfoSet = value
						};
						interfaceEntries.Add(interfaceEntry);
					}
				}
			}

			return interfaceEntries;
		}

		private static List<IInfo> GetInfoFromFields(object obj, FieldInfo[] infos)
		{
			var result = new List<IInfo>();
			foreach (FieldInfo info in infos)
			{
				object value = info.GetValue(obj);
				result.Add(new Info() { Name = info.Name, Value = value.ToString(), Type = SyntaxType.Field });
			}

			return result;
		}

		private static List<IInfo> GetInfoFromProperties(
			object obj,
			PropertyInfo[] infos)
		{
			var result = new List<IInfo>();
			foreach (PropertyInfo info in infos)
			{
				object value = info.GetValue(obj);
				result.Add(new Info() { Name = info.Name, Type = SyntaxType.Property, Value = value.ToString() });
			}

			return result;
		}
	}
}
