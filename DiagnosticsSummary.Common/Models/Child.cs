
namespace DiagnosticsSummary.Common.Models
{
    public class Child
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public GenderType Gender { get; set; }
        public int Age { get; set; }
        public AgeGroupType AgeGroup { get; set; }
        public string Group { get; set; }
        public DiagnosisType Diagnosis { get; set; }
    }
}