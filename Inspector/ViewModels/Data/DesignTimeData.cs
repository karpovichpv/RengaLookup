using Inspector.Model;

namespace Inspector.ViewModels.Data
{
    public class DesignTimeData : IData
    {
        public bool IsInterfaceHeader { get; set; }

        public bool IsSubHeader { get; set; }

        public string Label { get; set; } = "DataName";

        public string Value { get; set; } = "DataValue";

        public bool CanGet { get; set; }

        public IEnumerable<OutObject> WalkDown()
        {
            throw new NotImplementedException();
        }
    }
}
