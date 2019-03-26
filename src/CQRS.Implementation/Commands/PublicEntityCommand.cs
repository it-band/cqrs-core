namespace CQRS.Implementation.Commands
{
    public class PublicEntityCommand<TOut, TPublicId> : Implementation.Commands.CommandBase<TOut>
    {
        public TPublicId PublicId { get; set; }
    }
}
