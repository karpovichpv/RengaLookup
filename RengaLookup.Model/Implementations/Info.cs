using RengaLookup.Model.Contracts;

namespace RengaLookup.Model.Implementations
{
	public class Info : IInfo
	{
		public SyntaxType Type { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
}
