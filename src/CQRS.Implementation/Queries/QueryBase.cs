using System.Threading.Tasks;
using CQRS.Abstractions.Models;
using CQRS.Models;

namespace CQRS.Implementation.Queries
{
    public class QueryBase<TOut> : IQuery<Task<Result<TOut>>>
    {
    }
}
