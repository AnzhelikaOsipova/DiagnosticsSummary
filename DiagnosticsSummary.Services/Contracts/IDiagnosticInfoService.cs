using DiagnosticsSummary.Common.Models;
using LanguageExt;

namespace DiagnosticsSummary.Services.Contracts
{
    public interface IDiagnosticInfoService
    {
        public Task<Either<Exception, DiagnosticInfo>> FindAsync(string name);
        public Task<Either<Exception, List<DiagnosticInfo>>> GetAllAsync();
        public Task<Option<Exception>> CreateAsync(DiagnosticInfo newDiagnostic);
        public Task<Option<Exception>> UpdateAsync(DiagnosticInfo updatedDiagnostic);
        public Task<Option<Exception>> DeleteAsync(string name);
    }
}
