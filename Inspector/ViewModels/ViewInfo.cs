namespace Inspector.ViewModels
{
    public class ViewInfo
    {
        public string Name { get; init; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsMainHeader { get; set; }
        public bool IsSubHeader { get; set; }
    }
}
