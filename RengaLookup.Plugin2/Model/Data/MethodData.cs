using System.Reflection;

namespace RengaLookup.Plugin2.Model.Data
{
    public class MethodData : BaseData
    {
        private readonly object _fatherObject;
        private readonly MethodInfo _methodInfo;
        private readonly object _returnType;

        public MethodData(object fatherObject, MethodInfo info)
            : base(info.Name)
        {
            _fatherObject = fatherObject;
            _methodInfo = info;
        }

        private protected override bool CheckIfCanGet()
        {
            bool isParameterless = _methodInfo.GetParameters().Length == 0;
            if (isParameterless)
            {
                object result = _methodInfo.Invoke(_fatherObject, []);
                if (result != null)
                {
                    if (result is bool || result is double || result is int || result is string)
                    {
                        _object = result;
                        return false;
                    }
                    else
                    {
                        _object = _methodInfo.ReturnType;
                        return true;
                    }
                }
            }
            else
            {
                _object = _methodInfo.ReturnType;
            }
            return false;
        }
    }
}
