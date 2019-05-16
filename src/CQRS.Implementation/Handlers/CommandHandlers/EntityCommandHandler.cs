using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Abstractions.Models;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Models;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class EntityCommandHandler<TIn, TOut, TEntity> : CommandHandlerBase<TIn, TOut>
        where TIn : ICommand<Task<Result<TOut>>>
        where TEntity : class, IEntity
    {

        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        protected IQueryable<TEntity> Query;
        protected IEnumerable<IAccessFilter<TEntity>> AccessFilters;

        private void ApplyAccessFilters()
        {
            foreach (var accessFilter in AccessFilters)
            {
                Query = accessFilter.Apply(Query);
            }
        }

        protected EntityCommandHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters)
        {
            DbContext = dbContext;
            AccessFilters = accessFilters;
            DbSet = dbContext.Set<TEntity>();
            Query = DbSet;
            ApplyAccessFilters();
        }

        protected virtual Task<Result> OnBeforeAction(TEntity entity, TIn input)
        {
            return Task.FromResult(Result.Success());
        }

        protected virtual Task<Result> OnAfterAction(TEntity entity, TIn input)
        {
            return Task.FromResult(Result.Success());
        }
    }
}
