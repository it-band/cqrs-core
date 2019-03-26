namespace CQRS.Models.Commands
{
    public abstract class EntityCommand<TOut, TId> : CommandBase<TOut>
    {
        public TId Id { get; set; }
    }
}
