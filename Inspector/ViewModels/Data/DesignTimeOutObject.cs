using Inspector.Model;

namespace Inspector.ViewModels.Data
{
    class DesignTimeOutObject : IOutObject
    {
        public object Object { get; set; } = "DesignTimeObject";
        public string Name { get; set; } = "DesignTimeNameObject";
    }
}
