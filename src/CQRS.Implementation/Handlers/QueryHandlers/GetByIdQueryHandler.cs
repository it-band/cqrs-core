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
    public abstract class GetByIdQueryHandler<TIn, TOut, TEntity, TId> : EntityQueryHandler<TIn, TOut, TEntity>
        where TIn : GetByIdQuery<TOut, TId>
        where TEntity : class, IEntity<TId>
    {
        protected readonly IMapper Mapper;


        protected GetByIdQueryHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters, IMapper mapper) : base(dbContext, accessFilters)
        {
            Mapper = mapper;
        }

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            var value = await Query.Where(e => e.Id.Equals(input.Id)).ProjectTo<TOut>(Mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if (value == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name}' with Id: {input.Id} doesn't exist");
            }

            return value;
        }
    }
}
