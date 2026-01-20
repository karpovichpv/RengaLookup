
using System.Reflection;

namespace RengaLookup.Plugin2.Model.Data
{
    public class FieldData : BaseData
    {
        private readonly FieldInfo _info;
        private object _childObject;

        public FieldData(object fatherObject, FieldInfo info) : base(info.Name)
        {
            _object = fatherObject;
            _info = info;
        }

        public override List<object> WalkDown()
        {
            return [_childObject];
        }

        private protected override bool CheckIfCanGet()
        {
            if (_object != null && (_info.FieldType.FullName.Contains("Renga")))
            {
                _childObject = _info.GetValue(_object);
                return true;
            }

            return false;
        }

        private protected override string GetValue()
        {
            Type propertyType = _info.FieldType;
            if (propertyType == typeof(string)
                || propertyType == typeof(int)
                || propertyType == typeof(bool)
                || propertyType == typeof(byte)
                || propertyType == typeof(uint)
                || propertyType == typeof(double))
            {
                return _info.GetValue(_object).ToString(); ;
            }

            return propertyType.ToString();
        }
    }
}
