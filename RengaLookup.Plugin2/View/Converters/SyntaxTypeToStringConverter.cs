using RengaLookup.Model.Contracts;
using System.Globalization;
using System.Windows.Data;

namespace RengaLookup.Plugin2.View.Converters
{
    internal class SyntaxTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SyntaxType syntaxType)
            {
                return syntaxType.ToString();
            }

            return "Undefined type";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return SyntaxType.NotSet;
        }
    }
}
