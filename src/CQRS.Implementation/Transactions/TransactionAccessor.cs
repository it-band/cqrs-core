using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CQRS.Implementation.Transactions
{
    public class TransactionAccessor : ITransactionAccessor
    {
        protected DbContext DbContext { get; set; }

        public TransactionAccessor(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return await DbContext.Database.BeginTransactionAsync(isolationLevel);
        }
    }
}
}
