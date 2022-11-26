
using DiagnosticsSummary.Common.Models;
using LanguageExt;

namespace DiagnosticsSummary.Services.Contracts
{
    public interface IDiagnosticService
    {
        public Task<Either<Exception, List<Diagnostic>>> FindDiagnosticsAsync(DiagnosticFilter filter);
        public Task<Option<Exception>> CreateAsync(Diagnostic newDiagnostic);
        public Task<Option<Exception>> UpdateAsync(int diagnosticid, DiagnosticFilter updates);
        public Task<Option<Exception>> DeleteManyAsync(IEnumerable<int> ids);
        public Task<Option<Exception>> DeleteAsync(int diagnosticid);
    }
}
