using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Models;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class UpdateEntityCommandHandler<TIn, TEntity, TId> : EntityCommandHandler<TIn, bool, TEntity>
        where TIn : EntityCommand<bool, TId>
        where TEntity : class, IEntity<TId>
    {
        protected readonly IMapper Mapper;

        protected UpdateEntityCommandHandler(DbContext dbContext, IMapper mapper, IEnumerable<IAccessFilter<TEntity>> accessFilters) : base(dbContext, accessFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<bool>> Handle(TIn input)
        {
            var entity = await Query.FirstOrDefaultAsync(e => e.Id.Equals(input.Id));

            if (entity == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name}' with Id: {input.Id} doesn't exist");
            }

            entity = Mapper.Map(input, entity);

            await OnBeforeAction(entity, input);

            await DbContext.SaveChangesAsync();

            await OnAfterAction(entity, input);

            return true;
        }
    }
}
