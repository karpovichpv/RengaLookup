using Renga;
using Inspector.Model.Data;

namespace Inspector.Domain.Helpers
{
    internal class ParametersDataGetter
    {
        public static List<BaseData> GetParameters(IParameterContainer parameters)
        {
            List<BaseData> result = [];
            IGuidCollection ids = parameters.GetIds();
            for (int i = 0; i < ids.Count; ++i)
            {
                Guid id = ids.Get(i);
                IParameter parameter = parameters.Get(id);

                object? value = GetParameterValue(parameter);
                if (value != null)
                    result.Add(new UnitValue(parameter.Definition.Name, value));
            }

            return result;
        }

        private static object? GetParameterValue(IParameter param)
        {
            return param.ValueType switch
            {
                ParameterValueType.ParameterValueType_Double => param.GetDoubleValue(),
                ParameterValueType.ParameterValueType_String => param.GetStringValue(),
                ParameterValueType.ParameterValueType_Int => param.GetIntValue(),
                ParameterValueType.ParameterValueType_Bool => param.GetBoolValue(),
                _ => null,
            };
        }
    }
}
