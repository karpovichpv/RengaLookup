using Renga;
using RengaLookup.Plugin2.Domain.Helpers;
using RengaLookup.Plugin2.Domain.StylesExtensions;
using RengaLookup.Plugin2.Model;
using RengaLookup.Plugin2.Model.Data;
using RengaLookup.Plugin2.Model.Data.ReflectionData;
using System.Reflection;

namespace RengaLookup.Plugin2.Domain
{
    internal class ChiefCollector
    {
        private readonly object _modelObject;
        private readonly Type _type;
        private readonly Application _app;

        public ChiefCollector(object modelObject)
        {
            object resultedObject = modelObject is OutObject
                ? (modelObject as OutObject).Object
                : modelObject;
            _modelObject = resultedObject ?? throw new ArgumentNullException(nameof(resultedObject));
            _type = resultedObject.GetType();
            _app = new Application();
        }

        public IEnumerable<BaseData> Collect()
        {
            var result = new List<BaseData>();
            if (_modelObject is IParameterContainer parameterContainer)
                result = ParametersDataGetter.GetParameters(parameterContainer);
            else if (_modelObject is IPropertyContainer propertyContainer)
                result = PropertiesDataGetter.GetProperties(propertyContainer);
            else if (_modelObject is IQuantityContainer quantityContainer)
                result = QuantitiesDataGetter.GetQuantities(quantityContainer);
            else
            {
                bool isRengaObject = _type.FullName
                    .Contains("ComObject", StringComparison.InvariantCultureIgnoreCase);
                if (isRengaObject)
                    result.AddRange(CollectRengaInterfacesAndClasses());
                else
                {
                    result.AddRange(CollectOrdynaryProperties());
                    result.AddRange(CollectOrdynaryFields());
                    result.AddRange(CollectOrdynaryMethods());
                }
            }

            return result;
        }


        private List<BaseData> CollectRengaInterfacesAndClasses()
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

        private List<BaseData> CollectOrdynaryProperties()
        {
            PropertyInfo[] properties = _type.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic);

            var result = new List<BaseData>();
            if (properties.Length > 0)
                result.Add(SubHeaderData.GetPropertiesSubHeader());
            foreach (PropertyInfo info in properties)
                result.Add(new PropertyData(_modelObject, info));

            return result;
        }

        private List<BaseData> CollectOrdynaryFields()
        {

            FieldInfo[] fields = _type.GetFields(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic);
            var result = new List<BaseData>();
            if (fields.Length > 0)
                result.Add(SubHeaderData.GetFieldsSubHeader());

            foreach (FieldInfo field in fields)
                if (!field.Name.Contains("BackingField"))
                    result.Add(new FieldData(_modelObject, field));

            return result;
        }

        private List<BaseData> CollectOrdynaryMethods()
        {
            var result = new List<BaseData>();

            IEnumerable<MethodInfo> methods = _type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(t => !t.IsSpecialName && t.IsSecurityCritical && t.Name != "FromStruct" && t.Name != "ToStruct");

            if (methods.Count() > 0)
                result.Add(SubHeaderData.GetMethodsSubHeader());

            foreach (MethodInfo info in methods)
                result.Add(new MethodData(_modelObject, info));

            return result;
        }


        private static List<BaseData> GetInfoFromFields(object obj, FieldInfo[] infos)
        {
            var result = new List<BaseData>();
            if (infos.Length != 0)
                result.Add(SubHeaderData.GetFieldsSubHeader());

            foreach (FieldInfo info in infos)
                result.Add(new FieldData(obj, info));

            return result;
        }

        private static List<BaseData> GetInfoFromProperties(
            object obj,
            PropertyInfo[] infos)
        {
            var result = new List<BaseData>();
            if (infos.Length != 0)
                result.Add(SubHeaderData.GetPropertiesSubHeader());

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
                result.Add(SubHeaderData.GetMethodsSubHeader());
            foreach (MethodInfo info in infos)
            {
                result.Add(new MethodData(obj, info));
            }

            return result;
        }
    }
}
