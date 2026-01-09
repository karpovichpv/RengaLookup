using Renga;
using RengaLookup.Plugin2.Model.Data;
using System.Reflection;

namespace RengaLookup.Plugin2.Domain
{
    internal class DataCollector
    {
        private readonly IModelObject _modelObject;

        public DataCollector(IModelObject modelObject)
        {
            _modelObject = modelObject ?? throw new ArgumentNullException(nameof(modelObject));
        }

        public IEnumerable<BaseData> Collect()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName[] referencedAssemblies = executingAssembly.GetReferencedAssemblies();
            List<AssemblyName> interopAssemblies = referencedAssemblies
                .Where(a => a.FullName.Contains("Interop"))
                .ToList();

            var result = new List<BaseData>();
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
                        FieldInfo[] fieldInfos = @interface.GetFields();
                        MethodInfo[] methodInfos = @interface.GetMethods(BindingFlags.Public);
                        if (propertyInfos.Length + fieldInfos.Length + methodInfos.Length > 0)
                            result.Add(new InterfaceNameHeaderData(@interface.Name));

                        IEnumerable<BaseData> propretiesDataSet = GetInfoFromProperties(_modelObject, propertyInfos);
                        IEnumerable<BaseData> fieldsDataSet = GetInfoFromFields(_modelObject, fieldInfos);
                        IEnumerable<BaseData> methodDataSet = GetInfoFromMethods(_modelObject, methodInfos);

                        result.AddRange(propretiesDataSet);
                        result.AddRange(fieldsDataSet);
                        result.AddRange(methodDataSet);
                    }
                }
            }

            return result;
        }

        private static List<BaseData> GetInfoFromFields(object obj, FieldInfo[] infos)
        {
            var result = new List<BaseData>();
            if (infos.Length != 0)
                result.Add(new SubHeaderData("Fields"));

            foreach (FieldInfo info in infos)
            {
                object value = info.GetValue(obj);
                result.Add(new FieldData(info.Name) { Value = value.ToString() });
            }

            return result;
        }

        private static List<BaseData> GetInfoFromProperties(
            object obj,
            PropertyInfo[] infos)
        {
            var result = new List<BaseData>();
            if (infos.Length != 0)
                result.Add(new SubHeaderData("Properties"));

            foreach (PropertyInfo info in infos)
            {
                object value = info.GetValue(obj);
                result.Add(new PropertyData(info.Name) { Value = value.ToString() });
            }

            return result;
        }

        private static List<BaseData> GetInfoFromMethods(
            object obj,
            MethodInfo[] infos)
        {
            var result = new List<BaseData>();
            if (infos.Length != 0)
                result.Add(new SubHeaderData("Methods"));
            foreach (MethodInfo info in infos)
            {
                object value = info.ReturnType;
                result.Add(new MethodData(info.Name) { Value = value.ToString() });
            }

            return result;
        }
    }
}
