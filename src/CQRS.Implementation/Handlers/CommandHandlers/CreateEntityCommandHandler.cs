using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Implementation.Commands;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class CreateEntityCommandHandler<TIn, TEntity, TId> : EntityCommandHandler<TIn, TId, TEntity>
        where TIn : EntityCommand<TId, TId>
        where TEntity : class, IEntity<TId>
    {
        protected readonly IMapper Mapper;

        protected CreateEntityCommandHandler(DbContext dbContext, IMapper mapper, IEnumerable<Models.IAccessFilter<TEntity>> permissionFilters) : base(dbContext, permissionFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<TId>> Handle(TIn input)
        {
            var entity = Mapper.Map<TEntity>(input);

            await OnBeforeAction(entity, input);

            await DbSet.AddAsync(entity);

            await DbContext.SaveChangesAsync();

            await OnAfterAction(entity, input);

            return entity.Id;
        }
    }
}
