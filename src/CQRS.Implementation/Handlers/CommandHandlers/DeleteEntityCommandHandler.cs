using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Implementation.Commands;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class DeleteEntityCommandHandler<TIn, TEntity, TId> : EntityCommandHandler<TIn, bool, TEntity>
        where TEntity : class, IEntity<TId>
        where TIn : EntityCommand<bool, TId>
    {
        protected DeleteEntityCommandHandler(DbContext dbContext, IEnumerable<Models.IAccessFilter<TEntity>> accessFilters) : base(dbContext, accessFilters)
        {
        }

        public override async Task<Result<bool>> Handle(TIn input)
        {
            var entity = await Query.FirstOrDefaultAsync(e => e.Id.Equals(input.Id));

            if (entity == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name}' with Id: {input.Id} doesn't exist");
            }

            var onBeforeActionResult = await OnBeforeAction(entity, input);

            if (!onBeforeActionResult.IsSuccess)
            {
                return onBeforeActionResult.Failure;
            }

            DbSet.Remove(entity);

            await DbContext.SaveChangesAsync();

            var onAfterActionResult = await OnAfterAction(entity, input);

            if (!onAfterActionResult.IsSuccess)
            {
                return onAfterActionResult.Failure;
            }

            return true;
        }
    }
}
