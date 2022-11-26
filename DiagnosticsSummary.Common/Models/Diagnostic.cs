
namespace DiagnosticsSummary.Common.Models
{
    public class Diagnostic
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Year { get; set; }
        public YearTime YearPart { get; set; }
        public string InterpretedValue { get; set; } = "не интерпретировано";
    }
}
