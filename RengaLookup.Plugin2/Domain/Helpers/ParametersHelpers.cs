using Renga;

namespace RengaLookup.Plugin2.Domain.StylesExtensions
{
    internal class ParametersHelpers
    {
        public static List<LookupParameter> OutputParameters(IParameterContainer parameters)
        {
            List<LookupParameter> result = [];
            IGuidCollection ids = parameters.GetIds();
            for (int i = 0; i < ids.Count; ++i)
            {
                IParameter parameter = parameters.Get(ids.Get(i));
                result.Add(new LookupParameter(parameter, GetParameterValue(parameter)));
            }

            return result;
        }

        private static object GetParameterValue(Renga.IParameter param)
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
