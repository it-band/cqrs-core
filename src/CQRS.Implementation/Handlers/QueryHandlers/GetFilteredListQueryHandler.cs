using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Implementation.Extensions;
using CQRS.Implementation.Models;
using CQRS.Implementation.Queries;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.QueryHandlers
{
    public abstract class GetFilteredListQueryHandler<TIn, TOut, TEntity> : EntityQueryHandler<TIn, PagedList<TOut>, TEntity>
        where TIn : GetFilteredListQuery<TOut>
        where TEntity : class, IEntity
    {
        protected readonly IMapper Mapper;
        protected Expression<Func<TEntity, bool>> Expression { get; set; } = e => true;


        protected GetFilteredListQueryHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> accessFilters, IMapper mapper) : base(dbContext, accessFilters)
        {
            Mapper = mapper;
        }

        protected virtual void BuildQuery(TIn input)
        {
            Query = Query.Where(Expression);
        }

        protected virtual void Sort(Sorting sorting)
        {
        }

        public override async Task<Result<PagedList<TOut>>> Handle(TIn input)
        {
            BuildQuery(input);

            Sort(input.Sorting);

            int? totalCount = null;

            if (input.Paging.ItemsCount > 0)
            {
                totalCount = await Query.CountAsync();
            }

            Query = Query.ApplyPageFilter(input);

            var items = await Query.ProjectTo<TOut>(Mapper.ConfigurationProvider).ToListAsync();

            return new PagedList<TOut>(items, totalCount);
        }
    }
}
