using DiagnosticsSummary.Common.Contracts;
using static DiagnosticsSummary.Common.Models.Diagnostics.DiagnosticsLibrary;

namespace DiagnosticsSummary.Common.Models.Diagnostics
{
    public class DiagnosticAnxiety : IDiagnosticInfo
    {
        public string Name { get; } = DiagnosticNames[DiagnosticType.Anxiety];

        public string Apply(int value)
        {
            return value.ToString();
        }
    }
}
