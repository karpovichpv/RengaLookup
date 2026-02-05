using Renga;
using RengaLookup.Plugin2.Model.Data;
using System.Reflection;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal class QuantitiesDataGetter
    {
        public static List<BaseData> GetQuantities(IQuantityContainer container)
        {
            Type targetType = typeof(Renga.Quantities);
            PropertyInfo[] staticProps = targetType.GetProperties(
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            var result = new List<BaseData>() { };
            foreach (PropertyInfo prop in staticProps)
            {
                object? value = prop?.GetValue(null);

                if (value != null)
                {
                    string? propName = prop?.Name;

                    if (value is Guid guid && propName != null)
                    {
                        IQuantity pQuantity = container.Get(guid);
                        if (pQuantity is not null)
                        {
                            QuantityType type = pQuantity.Type;
                            switch (type)
                            {
                                case QuantityType.QuantityType_Count:
                                    result.Add(new UnitValue(propName, pQuantity.AsCount()));
                                    break;
                                case QuantityType.QuantityType_Area:
                                    result.Add(new UnitValue(propName, pQuantity.AsArea(AreaUnit.AreaUnit_Millimeters2)));
                                    break;
                                case QuantityType.QuantityType_Length:
                                    result.Add(new UnitValue(propName, pQuantity.AsLength(LengthUnit.LengthUnit_Millimeters)));
                                    break;
                                case QuantityType.QuantityType_Mass:
                                    result.Add(new UnitValue(propName, pQuantity.AsMass(MassUnit.MassUnit_Kilograms)));
                                    break;
                                case QuantityType.QuantityType_Volume:
                                    result.Add(new UnitValue(propName, pQuantity.AsVolume(VolumeUnit.VolumeUnit_Meters3)));
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
