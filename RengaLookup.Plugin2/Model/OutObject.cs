namespace RengaLookup.Plugin2.Model
{
    public class OutObject
    {
        public OutObject(object @object, string name)
        {
            Object = @object;
            Name = name;
        }

        public object Object { get; }
        public string Name { get; }
    }
}
