using System.Reflection;

namespace RengaLookup.Plugin2.Model.Data.ReflectionData
{
    public class MethodData : ReflectionBaseData
    {
        private readonly MethodInfo _info;

        public MethodData(object fatherObject, MethodInfo info)
            : base(fatherObject, info.Name)
        {
            _info = info;
            _returnType = _info.ReturnType;
            bool isParameterless = _info.GetParameters().Length == 0;
            bool isVoidReturn = _info.ReturnType.Name.Equals("Void", StringComparison.InvariantCulture);
            bool hasForbidenName = CheckIfHasForbidenName(info);
            if (isParameterless && !isVoidReturn && !hasForbidenName)
                _returnObject = _info.Invoke(_fatherObject, []);
        }

        private static bool CheckIfHasForbidenName(MethodInfo info)
        {
            return info.Name switch
            {
                var s when s.Contains("GetType") => true,
                var s when s.Contains("MemberwiseClone") => true,
                var s when s.Contains("GetCopy") => true,
                var s when s.Contains("GetTypeCode") => true,
                _ => false
            };
        }
    }
}
