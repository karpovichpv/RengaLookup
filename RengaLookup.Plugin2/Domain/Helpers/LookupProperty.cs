using Renga;

namespace RengaLookup.Plugin2.Domain.StylesExtensions
{
    internal class LookupProperty
    {
        public LookupProperty(IProperty property, object value)
        {
            Property = property;
            Value = value;
        }

        public IProperty Property { get; }
        public object Value { get; }
    }
}
