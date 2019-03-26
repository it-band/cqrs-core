using System.Threading.Tasks;
using CQRS.Abstractions.Models;

namespace CQRS.Models.Queries
{
    public class QueryBase<TOut> : IQuery<Task<Result<TOut>>>
    {
    }
}
