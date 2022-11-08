using DiagnosticsSummary.Common.Contracts;
using LanguageExt;
using System;

namespace DiagnosticsSummary.DAL
{
    public interface IRepository<T> : IDisposable
        where T : class, IHasKeys
    {
        Task<Option<Exception>> CreateAsync(T newEntity);
        Task<Option<Exception>> UpdateAsync(T updatedEntity);
        Task<Option<Exception>> UpsertAsync(T updatedEntity);
        Task<Option<Exception>> DeleteAsync(params object[] keys);
        Task<Either<Exception, T>> ReadAsync(params object[] keys);
        Task<Either<Exception, List<T>>> ReadAllAsync();
    }
}
