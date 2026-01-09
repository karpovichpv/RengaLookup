
namespace RengaLookup.Plugin2.Model.Data
{
    public class FieldData : BaseData
    {
        public FieldData(string label, object obj) : base(label)
        {
            _object = obj;
        }

        public override List<object> WalkDown()
        {
            return null;
        }

        private protected override bool CheckIfCanGet()
        {
            return false;
        }
    }
}
