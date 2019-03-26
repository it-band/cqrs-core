using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Implementation.Handlers.Base;
using CQRS.Models;
using CQRS.Models.Commands;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class EntityCommandHandler<TIn, TOut, TEntity> : EntityHandler<TIn, TOut, TEntity>, ICommandHandler<TIn, Task<Result<TOut>>>
        where TIn : CommandBase<TOut>
        where TEntity : class, IEntity
    {

        protected EntityCommandHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> permissionFilters) : base(dbContext, permissionFilters)
        {
        }

        protected virtual Task OnBeforeAction(TEntity entity, TIn input)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnAfterAction(TEntity entity, TIn input)
        {
            return Task.CompletedTask;
        }
    }
}
