using System.Collections.Generic;

namespace RengaLookup.Model
{
	public interface IInterfaceInfo
	{
		string Name { get; set; }
		IEnumerable<IInfo> InfoSet { get; set; }
	}
}
