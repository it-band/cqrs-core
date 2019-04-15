using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Handlers.Base;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public class DeletePublicEntityCommandHandler<TIn, TEntity, TPublicId> : EntityCommandHandler<TIn, bool, TEntity>
        where TIn : PublicEntityCommand<bool, TPublicId>
        where TEntity : class, IPublicEntity<TPublicId>

    {
        public DeletePublicEntityCommandHandler(DbContext dbContext, IEnumerable<Models.IAccessFilter<TEntity>> accessFilters) : base(dbContext, accessFilters)
        {
        }

        public override async Task<Result<bool>> Handle(TIn input)
        {
            var entity = await Query.FirstOrDefaultAsync(e => e.PublicId.Equals(input.PublicId));

            if (entity == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name} with Public Id: {input.PublicId} doesn't exist");
            }

            DbSet.Remove(entity);

            await DbContext.SaveChangesAsync();

            return true;
        }
    }
}
