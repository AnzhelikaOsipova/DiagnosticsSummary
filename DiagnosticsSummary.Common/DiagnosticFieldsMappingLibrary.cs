using DiagnosticsSummary.Common.Models;

namespace DiagnosticsSummary.Common
{
    public static class DiagnosticFieldsMappingLibrary
    {
        public static Dictionary<YearTime, string> YearPartDictionary =
            new Dictionary<YearTime, string>()
            {
                { YearTime.Start, "Начало" },
                { YearTime.End, "Конец" }
            };
    }
}
