using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.Common.Models
{
    public class ChildInfo : IHasIdProperty<int>
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
            Preschool,
            Any
        }

        public ChildInfo() { }

        public AgeGroupType ChooseAgeGroup(int age)
        {
            if (age >= 3 && age <= 4)
            {
                return AgeGroupType.Junior;
            }
            else if (age >= 4 && age <= 5)
            {
                return AgeGroupType.Middle;
            }
            else if (age >= 5 && age <= 6)
            {
                return AgeGroupType.Senior;
            }
            else if (age >= 6 && age <= 7)
            {
                return AgeGroupType.Preschool;
            }
            return AgeGroupType.Any;
        }
    }
}