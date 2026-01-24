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
            if (isParameterless)
                _returnObject = _info.Invoke(_fatherObject, []);
        }

        private protected override bool CheckIfCanGet()
        {
            return CheckIfCanGetInternal();
        }
    }
}
