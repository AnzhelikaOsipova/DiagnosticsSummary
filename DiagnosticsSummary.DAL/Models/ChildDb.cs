using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.DAL.Models
{
    public class ChildDb : IHasKeys
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string AgeGroup { get; set; }
        public string Group { get; set; }
        public string Diagnosis { get; set; }

        public IEnumerable<object> Keys => new object[] { Id };
    }
}
