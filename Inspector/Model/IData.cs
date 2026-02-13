namespace Inspector.Model
{
    public interface IData
    {
        bool IsInterfaceHeader { get; }
        bool IsSubHeader { get; }
        string Label { get; }
        string Value { get; }
        bool CanGet { get; }
        IEnumerable<OutObject> WalkDown();
    }
}
