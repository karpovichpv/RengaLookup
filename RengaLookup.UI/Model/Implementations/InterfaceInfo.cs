using RengaLookup.Model;
using System.Collections.Generic;

namespace RengaLookup.UI.Model.Implementations
{
	internal class InterfaceInfo : IInterfaceInfo
	{
		public string Name { get; set; }
		public IEnumerable<IInfo> InfoSet { get; set; }
	}
}
