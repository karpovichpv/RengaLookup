using System.Collections;

namespace RengaLookup.Plugin2.Model.Data
{
    public class PropertyData : BaseData
    {
        public PropertyData(string label, object obj) : base(label)
        {
            _object = obj;
        }

        private protected override bool CheckIfCanGet()
        {
            if (_object != null && (_object.GetType().ToString().Contains("Renga")))
                return true;
            else if (_object.GetType() == typeof(ArrayList) && ((ArrayList)_object).Count > 0)
                return true;

            return false;
        }
    }
}
