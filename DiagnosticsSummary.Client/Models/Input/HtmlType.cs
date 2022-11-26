namespace DiagnosticsSummary.Client.Models.Input
{
    public class HtmlType
    {
        public string Label { get; set; }
        public string? Value { get; set; }
        public HtmlMethod Method { get; set; }
        public string? InputType { get; set; }
        public SelectObject[] SelectObjects { get; set; }
        public Func<string, bool> IsValidValue { get; set; }
    }
}
