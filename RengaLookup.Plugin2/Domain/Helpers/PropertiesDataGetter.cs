using Renga;
using RengaLookup.Plugin2.Model.Data;

namespace RengaLookup.Plugin2.Domain.StylesExtensions
{
    internal class PropertiesDataGetter
    {
        public static List<BaseData> GetProperties(IPropertyContainer properties)
        {
            List<BaseData> result = [];
            var ids = properties.GetIds();
            for (int i = 0; i < ids.Count; ++i)
            {
                Renga.IProperty property = properties.Get(ids.Get(i));
                result.Add(new UnitValue(property.Name, GetPropertyValue(property)));
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
