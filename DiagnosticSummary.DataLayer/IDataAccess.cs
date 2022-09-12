using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.DataLayer
{
    public interface IDataAccess
    {
        public bool TryAdd<T, TId>(T itemToAdd) where T : class, IHasIdProperty<TId>;
        public bool TryDelete<T, TId>(TId id) where T : class, IHasIdProperty<TId>;
        public bool TryUpdate<T, TId>(TId id, T updatedItem) where T : class, IHasIdProperty<TId>;
        public bool TryGet<T, TId>(out List<T> items) where T : class, IHasIdProperty<TId>;
    }
}
