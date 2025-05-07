namespace RengaLookup.Plugin2.Domain.Model
{
	internal abstract class Data
	{
		public string Label { get; set; }
		public object Value { get; set; }

		private protected abstract string GetValueString();

		public override string ToString()
		{
			return $"{Label}: {GetValueString()}";
		}

	}
}
