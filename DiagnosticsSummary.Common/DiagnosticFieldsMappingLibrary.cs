using DiagnosticsSummary.Common.Models;

namespace DiagnosticsSummary.Common
{
    public static class DiagnosticFieldsMappingLibrary
    {
        public static Dictionary<Diagnostic.YearTime, string> YearPartDictionary =
            new Dictionary<Diagnostic.YearTime, string>()
            {
                { Diagnostic.YearTime.Start, "Начало" },
                { Diagnostic.YearTime.End, "Конец" }
            };
    }
}
