namespace RengaLookup.Model
{
	public interface IInfo
	{
		SyntaxType Type { get; set; }
		string Name { get; set; }
		string Value { get; set; }
	}
}