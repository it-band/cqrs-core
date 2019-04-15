using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Implementation.Commands;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public class UpdatePublicEntityCommandHandler<TIn, TEntity, TPublicId> : EntityCommandHandler<TIn, bool, TEntity>
        where TIn : PublicEntityCommand<bool, TPublicId>
        where TEntity : class, IPublicEntity<TPublicId>
    {
        protected readonly IMapper Mapper;

        protected UpdatePublicEntityCommandHandler(DbContext dbContext, IEnumerable<Models.IAccessFilter<TEntity>> accessFilters, IMapper mapper) : base(dbContext, accessFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<bool>> Handle(TIn input)
        {
            var entity = await Query.FirstOrDefaultAsync(e => e.PublicId.Equals(input.PublicId));

            if (entity == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name} with Public Id: {input.PublicId} doesn't exist");
            }

            entity = Mapper.Map(input, entity);

            await OnBeforeAction(entity, input);

            await DbContext.SaveChangesAsync();

            await OnAfterAction(entity, input);

            return true;
        }
    }
}
