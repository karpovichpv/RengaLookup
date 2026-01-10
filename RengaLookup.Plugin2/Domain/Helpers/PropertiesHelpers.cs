using Renga;

namespace RengaLookup.Plugin2.Domain.StylesExtensions
{
    internal static class PropertiesHelpers
    {
        public static List<LookupProperty> OutputProperties(IPropertyContainer properties)
        {
            List<LookupProperty> result = [];
            var ids = properties.GetIds();
            for (int i = 0; i < ids.Count; ++i)
            {
                Renga.IProperty property = properties.Get(ids.Get(i));
                object value = GetPropertyValue(property);
                result.Add(new LookupProperty(property, value));
            }

            return result;
        }

        private static object GetPropertyValue(IProperty prop)
        {
            return prop.Type switch
            {
                PropertyType.PropertyType_Angle => prop.GetAngleValue(AngleUnit.AngleUnit_Degrees),
                PropertyType.PropertyType_Double => prop.GetDoubleValue(),
                PropertyType.PropertyType_String => prop.GetStringValue(),
                PropertyType.PropertyType_Area => prop.GetAreaValue(AreaUnit.AreaUnit_Meters2),
                PropertyType.PropertyType_Boolean => prop.GetBooleanValue(),
                PropertyType.PropertyType_Enumeration => prop.GetEnumerationValue(),
                PropertyType.PropertyType_Integer => prop.GetIntegerValue(),
                PropertyType.PropertyType_Length => prop.GetLengthValue(LengthUnit.LengthUnit_Meters),
                PropertyType.PropertyType_Logical => prop.GetLogicalValue(),
                PropertyType.PropertyType_Mass => prop.GetMassValue(MassUnit.MassUnit_Kilograms),
                PropertyType.PropertyType_Volume => prop.GetVolumeValue(VolumeUnit.VolumeUnit_Meters3),
                _ => null,
            };
        }
    }
}
