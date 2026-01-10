
namespace RengaLookup.Plugin2.Model.Data
{
    public class SubHeaderData : BaseData
    {
        public SubHeaderData(string label) : base(label)
        {
            IsSubHeader = true;
        }

        public override List<object> WalkDown()
        {
            return null;
        }
    }
}
