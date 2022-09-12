using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.DataLayer
{
    public class DataAccess: IDataAccess
    {
        private readonly DbContextOptions _dbOptions;
        private ILogger _logger;

        public DataAccess(DbContextOptions options, ILogger logger)
        {
            _dbOptions = options;
            _logger = logger;
        }

        public bool TryAdd<T, TId>(T itemToAdd) where T: class, IHasIdProperty<TId>
        {
            using (var crud = new BasicCRUD<T,TId>(new DiagnosticContext(_dbOptions), _logger))
            {
                if (!crud.TryAdd(itemToAdd))
                {
                    return false;
                }
                return true;
            }
        }

        public bool TryDelete<T, TId>(TId id) where T : class, IHasIdProperty<TId>
        {
            using (var crud = new BasicCRUD<T,TId>(new DiagnosticContext(_dbOptions), _logger))
            {
                if (!crud.TryDelete(id))
                {
                    return false;
                }
                return true;
            }
        }

        public bool TryUpdate<T, TId>(TId id, T updatedItem) where T : class, IHasIdProperty<TId>
        {
            using (var crud = new BasicCRUD<T,TId>(new DiagnosticContext(_dbOptions), _logger))
            {
                if (!crud.TryUpdate(id, updatedItem))
                {
                    return false;
                }
                return true;
            }
        }

        public bool TryGet<T,TId>(out List<T> items) where T : class, IHasIdProperty<TId>
        {
            using (var crud = new BasicCRUD<T, TId>(new DiagnosticContext(_dbOptions), _logger))
            {
                if(!crud.TryGet(out IQueryable<T> qItems))
                {
                    items = null;
                    return false;
                }
                items = qItems.ToList();
                return true;
            }
        }
    }
}
