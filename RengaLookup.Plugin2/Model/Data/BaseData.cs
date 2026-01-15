

namespace RengaLookup.Plugin2.Model.Data
{
    public abstract class BaseData
    {
        private protected object _object;

        protected BaseData(string label)
        {
            Label = label;
        }

        protected BaseData(string label, object obj)
        {
            Label = label;
            _object = obj;
        }

        public bool IsInterfaceHeader { get; protected set; }
        public bool IsSubHeader { get; protected set; }
        public string Label { get; }
        public string Value => GetValue();
        public bool CanGet => CheckIfCanGet();
        public virtual List<object> WalkDown() => null;

        private protected virtual bool CheckIfCanGet() => false;

        private protected string GetValue()
        {
            if (_object is null)
                return string.Empty;

            return _object.ToString();
        }
    }
}
