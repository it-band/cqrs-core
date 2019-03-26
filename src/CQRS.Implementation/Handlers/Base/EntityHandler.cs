using System.Collections.Generic;
using System.Linq;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.Base
{
    public abstract class EntityHandler<TIn, TOut, TEntity> : HandlerBase<TIn, TOut>
        where TEntity : class, IEntity
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        protected IQueryable<TEntity> Query;
        protected IEnumerable<Models.IAccessFilter<TEntity>> AccessFilters;

        private void ApplyPermissionFilters()
        {
            foreach (var permissionFilter in AccessFilters)
            {
                Query = permissionFilter.Apply(Query);
            }
        }

        protected EntityHandler(DbContext dbContext, IEnumerable<Models.IAccessFilter<TEntity>> accessFilters)
        {
            DbContext = dbContext;
            AccessFilters = accessFilters;
            DbSet = dbContext.Set<TEntity>();
            Query = DbSet;
            ApplyPermissionFilters();
        }
    }
}
