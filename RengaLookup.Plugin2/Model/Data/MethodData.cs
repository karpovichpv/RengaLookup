namespace RengaLookup.Plugin2.Model.Data
{
    public class MethodData : BaseData
    {
        public MethodData(string label, object obj) : base(label)
        {
            _object = obj;
        }

        private protected override bool CheckIfCanGet()
        {
            return false;
        }
    }
}
