using Renga;

namespace RengaLookup.Plugin2.Domain.StylesExtensions
{
    internal class LookupParameter
    {
        public LookupParameter(IParameter parameter, object value)
        {
            Parameter = parameter;
            Value = value;
        }

        public IParameter Parameter { get; }
        public object Value { get; }
    }
}
