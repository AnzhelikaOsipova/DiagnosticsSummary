using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiagnosticsSummary.Common.Models.Diagnostics.DiagnosticsLibrary;

namespace DiagnosticsSummary.Common.Contracts
{
    public interface IDiagnosticInfo
    {
        public string Name { get; }
        public string Apply(int value);
    }
}
