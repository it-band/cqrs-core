using System.Collections.Generic;
using System.Linq;
using CQRS.Implementation.Models;
using CQRS.Implementation.Queries;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.QueryHandlers
{
    public abstract class EntityQueryHandler<TIn, TOut, TEntity> : QueryHandlerBase<TIn, TOut>
        where TIn : QueryBase<TOut>
        where TEntity : class, IEntity
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        protected IQueryable<TEntity> Query;
        protected IEnumerable<IAccessFilter<TEntity>> AccessFilters;

        private void ApplyAccessFilters()
        {
            foreach (var permissionFilter in AccessFilters)
            {
                Query = permissionFilter.Apply(Query);
            }
        }

        protected EntityQueryHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters)
        {
            DbContext = dbContext;
            AccessFilters = accessFilters;
            DbSet = dbContext.Set<TEntity>();
            Query = DbSet;
            ApplyAccessFilters();
        }
    }
}
