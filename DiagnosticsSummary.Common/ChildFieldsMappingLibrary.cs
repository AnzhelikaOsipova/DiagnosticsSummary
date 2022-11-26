using DiagnosticsSummary.Common.Models;

namespace DiagnosticsSummary.Common
{
    public static class ChildFieldsMappingLibrary
    {
        public static Dictionary<GenderType, string> GenderDictionary =
            new Dictionary<GenderType, string>()
            {
                { GenderType.Male, "Мужской" },
                { GenderType.Female, "Женский" }
            };

        public static Dictionary<AgeGroupType, string> AgeGroupDictionary =
            new Dictionary<AgeGroupType, string>()
            {
                { AgeGroupType.Junior, "Младшая" },
                { AgeGroupType.Middle, "Средняя" },
                { AgeGroupType.Senior, "Старшая" },
                { AgeGroupType.Preschool, "Подготовительная" }
            };

        public static Dictionary<DiagnosisType, string> DiagnosisDictionary =
            new Dictionary<DiagnosisType, string>()
            {
                { DiagnosisType.ZPR, "ЗПР" },
                { DiagnosisType.TNR, "ТНР" }
            };
    }
}
