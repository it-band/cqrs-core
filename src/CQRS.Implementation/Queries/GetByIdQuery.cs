using CQRS.Models;

namespace CQRS.Implementation.Queries
{
    public class GetByIdQuery<TOut, TId> : QueryBase<TOut>
    {
        public TId Id { get; set; }
    }

    public class GetByIdQuery<TOut> : GetByIdQuery<TOut, int>
    {

    }
}
