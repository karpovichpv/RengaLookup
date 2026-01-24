using Renga;
using RengaLookup.Plugin2.Domain.Helpers;

namespace RengaLookup.Plugin2.Model.Data.ReflectionData
{
    public abstract class ReflectionBaseData : BaseData
    {
        private protected readonly object _fatherObject;

        private protected List<object> _childObjects;
        private protected Type _returnType;
        private protected object _returnObject;

        protected ReflectionBaseData(object fatherObject, string label) : base(label)
        {
            _fatherObject = fatherObject;
        }

        public override List<object> WalkDown()
        {
            return _childObjects;
        }

        private protected bool CheckIfCanGetInternal()
        {
            if (_returnObject != null)
            {
                if (_returnObject
                    is bool
                    or double
                    or int
                    or string)
                {
                    _object = _returnObject;
                    return false;
                }
                else
                {
                    _object = _returnType;
                    if (_returnType == typeof(ILayerCollection))
                    {
                        var layerCollection = (ILayerCollection)_returnObject;
                        _childObjects = LayerCollectionGetter
                            .GetLayers(layerCollection);
                    }
                    else
                        _childObjects = [_returnObject];

                    return true;
                }
            }
            else
                _object = _returnType;

            return false;
        }
    }
}
