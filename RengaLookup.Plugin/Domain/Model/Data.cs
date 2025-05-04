namespace RengaLookup.Plugin.Domain.Model
{
	internal abstract class Data
	{
		public EntryType Type { get; set; }
		public string? Label { get; set; }
		public string? Value { get; set; }

		private protected abstract string GetValueString();

		public override string ToString()
		{
			return $"{Label}: {GetValueString()}";
		}

	}
}
