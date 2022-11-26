
namespace DiagnosticsSummary.Common.Models
{
    public class DiagnosticFilter
    {
        public int? ChildId { get; set; }
        public string? Name { get; set; }
        public int? Value { get; set; }
        public int? Year { get; set; }
        public YearTime? YearPart { get; set; }
    }
}
