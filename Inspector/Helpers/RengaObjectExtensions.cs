using Inspector.Model;

namespace Inspector.Helpers
{
    internal static class RengaObjectExtensions
    {
        public static List<OutObject> ToRengaObjects(this List<object> objects)
        {
            List<OutObject> result = [];
            foreach (object obj in objects)
                result.Add(new OutObject(obj, obj.GetType().Name));

            return result;
        }

        public static OutObject ToRengaObject(this object obj)
        {
            return new OutObject(obj, obj.GetType().Name);
        }
    }
}
