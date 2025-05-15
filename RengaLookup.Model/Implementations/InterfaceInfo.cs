using RengaLookup.Model.Contracts;
using System.Collections.Generic;

namespace RengaLookup.Model.Implementations
{
	public class InterfaceInfo : IInterfaceInfo
	{
		public string Name { get; set; }
		public IEnumerable<IInfo> InfoSet { get; set; }
	}
}
