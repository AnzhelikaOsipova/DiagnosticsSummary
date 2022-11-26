namespace DiagnosticsSummary.Client.Models.Input
{
    public class SelectObject
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static SelectObject[] Create(string[] values)
        {
            return values.Select(v => new SelectObject() { Name = v, Value = v }).ToArray();
        }
    }
}
