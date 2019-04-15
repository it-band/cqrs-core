using System.Threading.Tasks;
using CQRS.Abstractions.Models;
using CQRS.Models;

namespace CQRS.Implementation.Queries
{
    public class GetFilteredListQuery<TOut> : FilterBase, IQuery<Task<Result<PagedList<TOut>>>>
    {
    }
}
