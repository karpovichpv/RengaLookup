using Renga;
using RengaLookup.Plugin2.Domain.Helpers;
using RengaLookup.Plugin2.Domain.StylesExtensions;
using RengaLookup.Plugin2.Model.Data;
using System.Reflection;

namespace RengaLookup.Plugin2.Domain
{
    internal class ChiefCollector
    {
        private readonly object _modelObject;
        private readonly Application _app;

        public ChiefCollector(object modelObject)
        {
            _modelObject = modelObject
                ?? throw new ArgumentNullException(nameof(modelObject));
            _app = new Application();
        }

        public IEnumerable<BaseData> Collect()
        {
            List<BaseData> result;
            if (_modelObject is IParameterContainer parameterContainer)
                result = ParametersDataGetter.GetParameters(parameterContainer);
            else if (_modelObject is IPropertyContainer propertyContainer)
                result = PropertiesDataGetter.GetProperties(propertyContainer);
            else if (_modelObject is IQuantityContainer quantityContainer)
                result = QuantitiesDataGetter.GetQuantities(quantityContainer);
            else
                result = CollectInterfacesAndClasses();

            return result;
        }

        private List<BaseData> CollectInterfacesAndClasses()
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
                    .Where(t => t.IsInterface || t.IsClass || (t.IsValueType && !t.IsPrimitive && !t.IsEnum));
                foreach (Type @interface in interfaces)
                {
                    if (@interface.IsInstanceOfType(_modelObject))
                    {
                        PropertyInfo[] propertyInfos = @interface.GetProperties();
                        FieldInfo[] fieldInfos = @interface.GetFields();
                        MethodInfo[] methodInfos = @interface.GetMethods(
                            BindingFlags.Instance
                            | BindingFlags.Public
                            | BindingFlags.NonPublic
                            | BindingFlags.DeclaredOnly)
                            .Where(m => !m.IsSpecialName).ToArray();
                        if (propertyInfos.Length + fieldInfos.Length + methodInfos.Length > 0)
                            result.Add(new InterfaceHeaderData(@interface.Name));

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
                result.Add(new FieldData(info.Name, value));
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
                result.Add(new PropertyData(obj, info));
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
                result.Add(new MethodData(obj, info));
            }

            return result;
        }
    }
}
