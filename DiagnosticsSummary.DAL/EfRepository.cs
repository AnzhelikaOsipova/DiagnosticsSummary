using DiagnosticsSummary.Common.Contracts;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DiagnosticsSummary.DAL
{
    public class EfRepository<T>: IRepository<T>
        where T : class, IHasKeys
    {
        private readonly DbContext ctx;

        public EfRepository(DbContext context)
        {
            ctx = context;
        }

        public async Task<Option<Exception>> CreateAsync(T newEntity)
        {
            try
            {
                await ctx.Set<T>().AddAsync(newEntity);
                await ctx.SaveChangesAsync();
                return Option<Exception>.None;
            }
            catch (Exception e)
            {
                Log.Logger.Error("Unable to create an object {1}", typeof(T).Name);
                return e;
            }
        }

        public async Task<Option<Exception>> UpdateAsync(T updatedEntity)
        {
            try
            {
                ctx.Set<T>().Update(updatedEntity);
                await ctx.SaveChangesAsync();
                return Option<Exception>.None;
            }
            catch (Exception e)
            {
                Log.Logger.Error("Unable to update an object {1}", typeof(T).Name);
                return e;
            }
        }

        public async Task<Option<Exception>> UpsertAsync(T updatedEntity)
        {
            try
            {
                var res = await ctx.Set<T>().FindAsync(updatedEntity.Keys);
                if (res is null)
                {
                    await ctx.Set<T>().AddAsync(updatedEntity);
                }
                else
                {
                    ctx.Set<T>().Update(updatedEntity);
                }
                await ctx.SaveChangesAsync();
                return Option<Exception>.None;
            }
            catch (Exception e)
            {
                Log.Logger.Error("Unable to upsert an object {1}", typeof(T).Name);
                return e;
            }
        }

        public async Task<Option<Exception>> DeleteAsync(params object[] keys)
        {
            try 
            {
                var itemToDel = await ctx.Set<T>().FindAsync(keys);
                ctx.Set<T>().Remove(itemToDel);
                await ctx.SaveChangesAsync();
                return Option<Exception>.None;
            }
            catch (Exception e)
            {
                Log.Logger.Error("Unable to delete an object {1}", typeof(T).Name);
                return e;
            }
        }

        public async Task<Either<Exception, T>> ReadAsync(params object[] keys)
        {
            try
            {
                return await ctx.Set<T>().FindAsync(keys);
            }
            catch (Exception e)
            {
                Log.Logger.Error("Unable to read an object {1} with keys ", typeof(T).Name, string.Join(" + ", keys));
                return e;
            }
        }

        public async Task<Either<Exception, List<T>>> ReadAllAsync()
        {
            try
            {
                return await ctx.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                Log.Logger.Error("Unable to read objects {1}", typeof(T).Name);
                return e;
            }
        }

        public void Dispose()
        {
            ctx?.Dispose();
        }
    }
}
