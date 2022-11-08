using DiagnosticsSummary.Common.Models;

namespace DiagnosticsSummary.Common
{
    public static class ChildFieldsMappingLibrary
    {
        public static Dictionary<Child.GenderType, string> GenderDictionary =
            new Dictionary<Child.GenderType, string>()
            {
                { Child.GenderType.Male, "Мужской" },
                { Child.GenderType.Female, "Женский" }
            };

        public static Dictionary<Child.AgeGroupType, string> AgeGroupDictionary =
            new Dictionary<Child.AgeGroupType, string>()
            {
                { Child.AgeGroupType.Junior, "Младшая" },
                { Child.AgeGroupType.Middle, "Средняя" },
                { Child.AgeGroupType.Senior, "Старшая" },
                { Child.AgeGroupType.Preschool, "Подготовительная" }
            };

        public static Dictionary<Child.DiagnosisType, string> DiagnosisDictionary =
            new Dictionary<Child.DiagnosisType, string>()
            {
                { Child.DiagnosisType.ZPR, "ЗПР" },
                { Child.DiagnosisType.TNR, "ТНР" }
            };
    }
}
