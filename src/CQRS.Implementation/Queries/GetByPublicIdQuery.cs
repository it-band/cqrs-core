using CQRS.Models;

namespace CQRS.Implementation.Queries
{
    public class GetByPublicIdQuery<TOut, TPublicId> : QueryBase<TOut>
    {
        public TPublicId PublicId { get; set; }
    }

    public class GetByPublicIdQuery<TOut> : GetByPublicIdQuery<TOut, string>
    {
    }
}
