using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Implementation.Handlers.Base;
using CQRS.Implementation.Queries;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.QueryHandlers
{
    public abstract class EntityQueryHandler<TIn, TOut, TEntity> : EntityHandler<TIn, TOut, TEntity>, IQueryHandler<TIn, Task<Result<TOut>>>
        where TIn : QueryBase<TOut>
        where TEntity : class, IEntity
    {
        protected EntityQueryHandler(DbContext dbContext, IEnumerable<Models.IAccessFilter<TEntity>> accessFilters) : base(dbContext, accessFilters)
        {
        }
    }
}
