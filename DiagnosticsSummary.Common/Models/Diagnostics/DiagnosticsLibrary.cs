using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticsSummary.Common.Models.Diagnostics
{
    public static class DiagnosticsLibrary
    {
        public static Dictionary<DiagnosticType, string> DiagnosticNames = new Dictionary<DiagnosticType, string>()
        {
            { DiagnosticType.Anxiety, "Тревожность" }
        };

        public enum DiagnosticType
        {
            Anxiety
        }
    }
}
