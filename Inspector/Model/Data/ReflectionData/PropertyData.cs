using System.Reflection;

namespace Inspector.Model.Data.ReflectionData
{
    public class PropertyData : ReflectionBaseData
    {
        private readonly PropertyInfo _info;
        public PropertyData(object fatherObject, PropertyInfo info)
            : base(fatherObject, info.Name)
        {
            _object = fatherObject;
            _info = info;
            _returnType = info.PropertyType;

            if (fatherObject != null)
                _returnObject = _info.GetValue(fatherObject, null);
        }
    }
}
