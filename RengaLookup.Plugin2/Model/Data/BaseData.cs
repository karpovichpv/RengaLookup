namespace RengaLookup.Plugin2.Model.Data
{
    public abstract class BaseData
    {
        protected BaseData(string label)
        {
            Label = label;
        }

        public bool IsInterfaceHeader { get; protected set; }
        public bool IsSubHeader { get; protected set; }
        public string Label { get; }
        public string Value { get; set; }

    }
}
