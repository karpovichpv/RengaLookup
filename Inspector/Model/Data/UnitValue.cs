namespace Inspector.Model.Data
{
    internal class UnitValue : BaseData
    {
        public UnitValue(string label, object value) : base(label, value) { }
        public UnitValue(string label, int value) : base(label, value) { }
        public UnitValue(string label, double value) : base(label, value) { }
        public UnitValue(string label, bool value) : base(label, value) { }
    }
}
