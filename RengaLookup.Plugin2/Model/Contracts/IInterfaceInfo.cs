using RengaLookup.Model.Contracts;

namespace RengaLookup.Plugin2.Model.Contracts
{
    public interface IInterfaceInfo
    {
        string Name { get; set; }
        IEnumerable<IInfo> InfoSet { get; set; }
    }
}
