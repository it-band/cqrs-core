using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Implementation.Handlers.Base;
using CQRS.Models;
using CQRS.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.QueryHandlers
{
    public abstract class EntityQueryHandler<TIn, TOut, TEntity> : EntityHandler<TIn, TOut, TEntity>, IQueryHandler<TIn, Task<Result<TOut>>>
        where TIn : QueryBase<TOut>
        where TEntity : class, IEntity
    {
        protected EntityQueryHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters) : base(dbContext, accessFilters)
        {
        }
    }
}
