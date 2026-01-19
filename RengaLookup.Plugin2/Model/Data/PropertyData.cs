using System.Reflection;

namespace RengaLookup.Plugin2.Model.Data
{
    public class PropertyData : BaseData
    {
        private readonly PropertyInfo _info;
        private object _childObject;

        public PropertyData(object fatherObject, PropertyInfo info)
            : base(info.Name)
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
            if (_object != null && (_info.PropertyType.FullName.Contains("Renga")))
            {
                _childObject = _info.GetValue(_object, null);
                return true;
            }

            return false;
        }

        private protected override string GetValue()
        {
            Type propertyType = _info.PropertyType;
            if (propertyType == typeof(string)
                || propertyType == typeof(int)
                || propertyType == typeof(bool)
                || propertyType == typeof(byte)
                || propertyType == typeof(uint)
                || propertyType == typeof(double))
            {
                return _info.GetValue(_object, null).ToString(); ;
            }

            return _info.PropertyType.ToString();
        }
    }
}
