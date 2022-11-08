using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.DAL.Models
{
    public class DiagnosticDb : IHasKeys
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Year { get; set; }
        public string YearPart { get; set; }
        public IEnumerable<object> Keys => new object[] { Id };
    }
}
