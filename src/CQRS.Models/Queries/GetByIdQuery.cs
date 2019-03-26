namespace CQRS.Models.Queries
{
    public class GetByIdQuery<TOut, TId> : QueryBase<TOut>, IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
