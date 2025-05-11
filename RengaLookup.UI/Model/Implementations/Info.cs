using RengaLookup.Model;

namespace RengaLookup.UI.Model.Implementations
{
	internal class Info : IInfo
	{
		public SyntaxType Type { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
}
