using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Models;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class DeletePublicEntityCommandHandler<TIn, TEntity, TPublicId> : EntityCommandHandler<TIn, bool, TEntity>
        where TIn : PublicEntityCommand<bool, TPublicId>
        where TEntity : class, IPublicEntity<TPublicId>

    {
        protected DeletePublicEntityCommandHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters) : base(dbContext, accessFilters)
        {
        }

        public override async Task<Result<bool>> Handle(TIn input)
        {
            var entity = await Query.FirstOrDefaultAsync(e => e.PublicId.Equals(input.PublicId));

            if (entity == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name} with Public Id: {input.PublicId} doesn't exist");
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
