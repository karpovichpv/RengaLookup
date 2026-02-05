using Renga;
using RengaLookup.Plugin2.Model.Data;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal class PropertiesDataGetter
    {
        public static List<BaseData> GetProperties(IPropertyContainer properties)
        {
            List<BaseData> result = [];
            var ids = properties.GetIds();
            for (int i = 0; i < ids.Count; ++i)
            {
                IProperty property = properties.Get(ids.Get(i));
                object? value = GetPropertyValue(property);
                if (value != null)
                    result.Add(new UnitValue(property.Name, value));
            }

            return result;
        }

        private static object? GetPropertyValue(IProperty prop)
        {
            return prop?.Type switch
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
                PropertyType.PropertyType_Undefined => throw new NotImplementedException(),
                _ => null,
            };
        }
    }
}
