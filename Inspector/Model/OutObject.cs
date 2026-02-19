namespace Inspector.Model
{
    public class OutObject : IOutObject
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
