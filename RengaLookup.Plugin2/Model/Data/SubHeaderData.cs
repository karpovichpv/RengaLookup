
namespace RengaLookup.Plugin2.Model.Data
{
    public class SubHeaderData : BaseData
    {
        public SubHeaderData(string label) : base(label)
        {
            IsSubHeader = true;
        }

        public static BaseData GetMethodsSubHeader()
            => new SubHeaderData("Methods");

        public static BaseData GetPropertiesSubHeader()
            => new SubHeaderData("Properties");

        public static BaseData GetFieldsSubHeader()
            => new SubHeaderData("Fields");
    }
}
