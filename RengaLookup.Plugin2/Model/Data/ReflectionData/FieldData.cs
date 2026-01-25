using System.Reflection;

namespace RengaLookup.Plugin2.Model.Data.ReflectionData
{
    public class FieldData : ReflectionBaseData
    {
        private readonly FieldInfo _info;
        private object _childObject;

        public FieldData(object fatherObject, FieldInfo info)
            : base(fatherObject, info.Name)
        {
            _object = fatherObject;
            _info = info;
            _returnType = _info.FieldType;
            _returnObject = _info.GetValue(_object);
        }
    }
}
