using Renga;
using RengaLookup.Plugin2.Domain.Helpers;

namespace RengaLookup.Plugin2.Model.Data.ReflectionData
{
    public abstract class ReflectionBaseData : BaseData
    {
        private protected readonly object _fatherObject;

        private protected IEnumerable<OutObject> _childObjects;
        private protected Type _returnType;
        private protected object _returnObject;

        protected ReflectionBaseData(object fatherObject, string label)
            : base(label)
        {
            if (fatherObject is OutObject outObject)
                _fatherObject = outObject;
            else
                _fatherObject = fatherObject;
        }

        public override IEnumerable<OutObject> WalkDown()
        {
            return _childObjects;
        }

        private protected override bool CheckIfCanGet()
        {
            if (_returnObject != null)
            {
                if (_returnObject
                    is bool
                    or double
                    or int
                    or byte
                    or Int16
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
                    else if (_returnType == typeof(IModelObjectCollection))
                    {
                        var collection = (IModelObjectCollection)_returnObject;
                        _childObjects = ModelObjectGetter
                            .GetObjects(collection);
                    }
                    else
                        _childObjects = [
                            new OutObject(_returnObject, _returnType.Name)];

                    return true;
                }
            }
            else
                _object = _returnType;

            return false;
        }
    }
}
