
using DiagnosticsSummary.Common.Models;
using LanguageExt;

namespace DiagnosticsSummary.Services.Contracts
{
    public interface IChildService
    {
        public Task<Either<Exception, List<Child>>> FindChildrenAsync(ChildFilter filter);
        public Task<Option<Exception>> CreateAsync(Child newChild);
        public Task<Option<Exception>> UpdateAsync(int childId, ChildFilter updates);
        public Task<Option<Exception>> DeleteAsync(int childid);
    }
}
