using System.Reflection;

namespace Inspector.Model.Data.ReflectionData
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
            bool hasForbidenName = ForbidenMethodNames.Any(n => n.Contains(info.Name));
            if (isParameterless && !isVoidReturn && !hasForbidenName)
            {
                _returnObject = _info.Invoke(_fatherObject, []);
            }
            else if (info.Name is "GetProfilePlacementOnBaseline")
            {
                _returnObject = _info.Invoke(_fatherObject, [0d]);
            }
        }

        private static string[] ForbidenMethodNames =>
            [
            "MemberwiseClone",
            "GetCopy",
            "GetTransformFrom",
            "GetTransformInto",
            "GetTypeCode",
            "ToByteArray",
            "NewGuid",
            "CreateOperation",
            "CreateNewEntityArgs",
            "GetEnumerator",
            "Enable",
            "Disable",
            "CreateProject",
            "Quit",
            "Save",
            "CreateOperation",
            "StartOperation",
        ];
    }
}
