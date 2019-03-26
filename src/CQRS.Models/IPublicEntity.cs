namespace CQRS.Models
{
    public interface IPublicEntity<TPublicId> : IEntity
    {
        TPublicId PublicId { get; set; }
    }
}
