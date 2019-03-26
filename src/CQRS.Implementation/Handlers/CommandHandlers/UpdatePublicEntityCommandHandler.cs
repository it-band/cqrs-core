using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Implementation.Handlers.Base;
using CQRS.Models;
using CQRS.Models.Commands;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public class UpdatePublicEntityCommandHandler<TIn, TEntity, TPublicId> : EntityCommandHandler<TIn, TPublicId, TEntity>
        where TIn : PublicEntityCommand<TPublicId, TPublicId>
        where TEntity : class, IPublicEntity<TPublicId>
    {
        protected readonly IMapper Mapper;

        protected UpdatePublicEntityCommandHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> permissionFilters, IMapper mapper) : base(dbContext, permissionFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<TPublicId>> Handle(TIn input)
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

            return entity.PublicId;
        }
    }
}
