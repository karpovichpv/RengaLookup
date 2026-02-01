using Renga;
using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Domain.Helpers
{
    internal static class PolyCurve2dGetter
    {
        public static List<OutObject> GetCurves(IPolyCurve2D curve)
        {
            List<OutObject> result = [];
            for (int i = 0; i < curve.GetSegmentCount(); ++i)
            {
                ICurve2D segment = curve.GetSegment(i);
                result.Add(new OutObject(segment, $"{typeof(ICurve2D).ToString()} Id: {i}"));
            }

            return result;
        }
    }
}
