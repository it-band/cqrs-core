using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Implementation.Queries;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.QueryHandlers
{
    public abstract class GetByPublicIdQueryHandler<TIn, TOut, TEntity, TPublicId> : EntityQueryHandler<TIn, TOut, TEntity>
        where TIn : GetByPublicIdQuery<TOut, TPublicId>
        where TEntity : class, IPublicEntity<TPublicId>
    {
        protected readonly IMapper Mapper;

        protected GetByPublicIdQueryHandler(DbContext dbContext, IEnumerable<Models.IAccessFilter<TEntity>> permissionFilters, IMapper mapper) : base(dbContext, permissionFilters)
        {
            Mapper = mapper;
        }


        public override async Task<Result<TOut>> Handle(TIn input)
        {
            var query = Query.Where(e => e.PublicId.Equals(input.PublicId));

            if (await query.CountAsync() == 0)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name}' with Public Id: {input.PublicId} doesn't exist");
            }

            return await query.ProjectTo<TOut>(Mapper.ConfigurationProvider).FirstAsync();
        }
    }
}
