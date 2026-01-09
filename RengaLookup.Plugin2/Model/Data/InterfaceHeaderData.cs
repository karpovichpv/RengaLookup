
namespace RengaLookup.Plugin2.Model.Data
{
    public class InterfaceHeaderData : BaseData
    {
        public InterfaceHeaderData(string label) : base(label)
        {
            IsInterfaceHeader = true;
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
