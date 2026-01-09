namespace RengaLookup.Plugin2.Model.Data
{
    public abstract class BaseInfo
    {
        protected BaseInfo(string label)
        {
            Label = label;
        }

        public bool IsInterfaceNameHeader { get; protected set; }
        public bool IsSubHeader { get; protected set; }
        public string Label { get; }
        public string Value { get; protected set; }

    }
}
