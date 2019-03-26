namespace CQRS.Models.Queries
{
    public class GetByPublicIdQuery<TOut, TPublicId> : QueryBase<TOut>, IPublicEntity<TPublicId>
    {
        public TPublicId PublicId { get; set; }
    }
}
