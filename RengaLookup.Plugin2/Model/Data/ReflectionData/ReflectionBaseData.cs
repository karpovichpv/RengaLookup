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
                if (_returnObject is int id)
                {
                    _object = _returnObject;
                    if (Label == "LevelId")
                    {
                        IModelObject level = ModelObjectGetter.GetObject(id);
                        if (level != null)
                        {
                            _childObjects = [
                                new OutObject(level, level.Name)];
                            return true;
                        }
                    }
                    else if (Label == "MaterialId")
                    {
                        IMaterial material = MaterialGetter.GetMaterial(id);
                        if (material != null)
                        {
                            _childObjects = [
                                new OutObject(material, material.Name)];
                            return true;
                        }
                    }
                    else if (Label == "GetSegmentCount" && _fatherObject is IPolyCurve2D curve)
                    {
                        List<OutObject> curves = PolyCurve2dGetter.GetCurves(curve);
                        _childObjects = [.. curves];
                        return true;
                    }
                    else if (Label == "StyleId" && _fatherObject is IBeamParams beamParams)
                    {
                        OutObject outObject = BeamStyleGetter.GetStyle(beamParams.StyleId);
                        _childObjects = [outObject];
                        return true;
                    }

                    return false;
                }
                else if (_returnObject
                     is bool
                     or double
                     or byte
                     or int
                     or Int16
                     or UInt16
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
                    else if (_returnType == typeof(IRebarUsageCollection))
                    {
                        var collection = (IRebarUsageCollection)_returnObject;
                        _childObjects = RebarUsageCollection
                            .GetUsages(collection);
                    }
                    else if (_returnType == typeof(IPlacement3DCollection))
                    {
                        var collection = (IPlacement3DCollection)_returnObject;
                        _childObjects = Placement3dGetter.Get(collection);
                    }
                    else if (_returnType == typeof(IRegion2DCollection))
                    {
                        var collection = (IRegion2DCollection)_returnObject;
                        _childObjects = Region2DGetter.Get(collection);
                    }
                    else
                    {
                        _childObjects = [
                            new OutObject(_returnObject, _returnType.Name)];
                    }

                    return true;
                }
            }
            else
                _object = _returnType;

            return false;
        }
    }
}
