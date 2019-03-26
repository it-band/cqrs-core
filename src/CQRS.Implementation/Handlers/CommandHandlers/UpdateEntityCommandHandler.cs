using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Models;
using CQRS.Models.Commands;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class UpdateEntityCommandHandler<TIn, TEntity, TId> : EntityCommandHandler<TIn, TId, TEntity>
        where TIn : EntityCommand<TId, TId>
        where TEntity : class, IEntity<TId>
    {
        protected readonly IMapper Mapper;

        protected UpdateEntityCommandHandler(DbContext dbContext, IMapper mapper, IEnumerable<IAccessFilter<TEntity>> permissionFilters) : base(dbContext, permissionFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<TId>> Handle(TIn input)
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

            return entity.Id;
        }
    }
}
