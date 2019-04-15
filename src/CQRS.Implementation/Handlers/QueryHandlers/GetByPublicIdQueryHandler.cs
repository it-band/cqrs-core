using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Implementation.Models;
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

        protected GetByPublicIdQueryHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters, IMapper mapper) : base(dbContext, accessFilters)
        {
            Mapper = mapper;
        }


        public override async Task<Result<TOut>> Handle(TIn input)
        {
            var value = await Query.Where(e => e.PublicId.Equals(input.PublicId))
                .ProjectTo<TOut>(Mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if (value == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name}' with Public Id: {input.PublicId} doesn't exist");
            }

            return value;
        }
    }
}
