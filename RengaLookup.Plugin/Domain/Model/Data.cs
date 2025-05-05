namespace RengaLookup.Plugin.Domain.Model
{
	internal abstract class Data
	{
		public required string? Label { get; init; }
		public required object? Value { get; init; }

		private protected abstract string GetValueString();

		public override string ToString()
		{
			return $"{Label}: {GetValueString()}";
		}

	}
}
