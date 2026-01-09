using RengaLookup.Plugin2.Model;

namespace RengaLookup.Plugin2.Helpers
{
    internal static class RengaObjectExtensions
    {
        public static List<RengaObject> ToRengaObjects(this List<object> objects)
        {
            List<RengaObject> result = [];
            foreach (object obj in objects)
                result.Add(new RengaObject() { Name = obj.GetType().Name, Object = obj });

            return result;
        }

        public static RengaObject ToRengaObject(this object obj)
        {
            return new RengaObject() { Name = obj.GetType().Name, Object = obj };
        }
    }
}
