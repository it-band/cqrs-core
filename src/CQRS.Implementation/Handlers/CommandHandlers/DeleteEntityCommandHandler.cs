﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Implementation.Handlers.Base;
using CQRS.Models;
using CQRS.Models.Commands;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public class DeleteEntityCommandHandler<TIn, TEntity, TId> : EntityCommandHandler<TIn, bool, TEntity>
        where TEntity : class, IEntity<TId>
        where TIn : EntityCommand<bool, TId>
    {
        public DeleteEntityCommandHandler(DbContext dbContext, IEnumerable<IAccessFilter<TEntity>> permissionFilters) : base(dbContext, permissionFilters)
        {
        }

        public override async Task<Result<bool>> Handle(TIn input)
        {
            var entity = await Query.FirstOrDefaultAsync(e => e.Id.Equals(input.Id));

            if (entity == null)
            {
                return Result.NotFound($"Entity '{typeof(TEntity).Name}' with Id: {input.Id} doesn't exist");
            }

            DbContext.Remove(entity);

            await DbContext.SaveChangesAsync();

            return true;
        }
    }
}