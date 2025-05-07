using RengaLookup.Plugin.Domain.Model;
using System.Collections.Generic;

namespace RengaLookup.Plugin.Domain.Model
{
	internal class InterfaceEntry
	{
		public IEnumerable<Data> Infos { get; set; }
		public string Name { get; set; }
	}
}
