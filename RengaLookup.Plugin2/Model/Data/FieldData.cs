
namespace RengaLookup.Plugin2.Model.Data
{
    public class FieldData : BaseData
    {
        public FieldData(string label, object obj) : base(label)
        {
            _object = obj;
        }
    }
}
