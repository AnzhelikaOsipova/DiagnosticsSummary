namespace DiagnosticsSummary.Client.Models
{
    public class HtmlType
    {
        public string Label { get; set; }
        public string? Value { get; set; }
        public Method HtmlMethod { get; set; }
        public string? InputType { get; set; }
        public SelectObject[] SelectObjects { get; set; }
        public Func<string,bool> IsValidValue { get; set; }
        public enum Method
        {
            Select,
            Input
        };

        public class SelectObject
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
