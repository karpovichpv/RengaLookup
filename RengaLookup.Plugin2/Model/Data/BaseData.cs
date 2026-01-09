

namespace RengaLookup.Plugin2.Model.Data
{
    public abstract class BaseData
    {
        private protected object _object;

        protected BaseData(string label)
        {
            Label = label;
        }

        public bool IsInterfaceHeader { get; protected set; }
        public bool IsSubHeader { get; protected set; }
        public string Label { get; }
        public string Value => GetValue();
        public bool CanGet => CheckIfCanGet();
        public abstract List<object> WalkDown();

        private protected abstract bool CheckIfCanGet();

        private protected string GetValue()
        {
            if (_object is null)
                return string.Empty;

            return _object.ToString();
        }
    }
}
