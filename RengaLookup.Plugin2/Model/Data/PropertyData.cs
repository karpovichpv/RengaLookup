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
            _fatherObject = fatherObject;
            _info = info;
        }

        public override List<object> WalkDown()
        {
            return [_childObject];
        }


        private protected override bool CheckIfCanGet()
        {
            if (_fatherObject != null && (_info.PropertyType.FullName.Contains("Renga")))
            {
                _childObject = _info.GetValue(_fatherObject, null);
                return true;
            }

            return false;
        }
    }
}
