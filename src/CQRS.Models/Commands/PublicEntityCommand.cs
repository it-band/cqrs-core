namespace CQRS.Models.Commands
{
    public class PublicEntityCommand<TOut, TPublicId> : CommandBase<TOut>
    {
        public TPublicId PublicId { get; set; }
    }
}
