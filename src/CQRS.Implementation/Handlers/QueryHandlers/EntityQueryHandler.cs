using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Abstractions.Models;
using CQRS.Implementation.Models;
using CQRS.Implementation.Queries;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.QueryHandlers
{
    public abstract class EntityQueryHandler<TIn, TOut, TEntity> : QueryHandlerBase<TIn, TOut>
        where TIn : IQuery<Task<Result<TOut>>>
        where TEntity : class, IEntity
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        protected IQueryable<TEntity> Query;
        protected IEnumerable<IAccessFilter<TEntity>> AccessFilters;

        private async Task ApplyAccessFilters()
        {
            foreach (var permissionFilter in AccessFilters)
            {
                Query = await permissionFilter.Apply(Query);
            }
        }

        protected EntityQueryHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters)
        {
            DbContext = dbContext;
            AccessFilters = accessFilters;
            DbSet = dbContext.Set<TEntity>();
            Query = DbSet;
            Task.FromResult(ApplyAccessFilters());
        }
    }
}
