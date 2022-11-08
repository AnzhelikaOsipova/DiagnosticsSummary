using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.Common.Models
{
    public class Child
    {
        public int Id { get; set; }
        public string? FIO { get; set; }
        public GenderType? Gender { get; set; }
        public int? Age { get; set; }
        public AgeGroupType? AgeGroup { get; set; }
        public string? Group { get; set; }
        public DiagnosisType? Diagnosis { get; set; }

        public enum DiagnosisType
        {
            TNR,
            ZPR
        }

        public enum GenderType
        {
            Male,
            Female
        }

        public enum AgeGroupType
        {
            Junior,
            Middle,
            Senior,
            Preschool
        }
    }
}