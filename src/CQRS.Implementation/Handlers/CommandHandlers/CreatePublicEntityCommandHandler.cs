using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Models;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class CreatePublicEntityCommandHandler<TIn, TEntity, TPublicId> : EntityCommandHandler<TIn, TPublicId, TEntity>
        where TIn : CommandBase<TPublicId>
        where TEntity : class, IPublicEntity<TPublicId>
    {
        protected readonly IMapper Mapper;

        protected CreatePublicEntityCommandHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> permissionFilters, IMapper mapper) : base(dbContext, permissionFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<TPublicId>> Handle(TIn input)
        {
            var entity = Mapper.Map<TEntity>(input);

            await OnBeforeAction(entity, input);

            await DbSet.AddAsync(entity);

            await DbContext.SaveChangesAsync();

            await OnAfterAction(entity, input);

            return entity.PublicId;
        }
    }
}
