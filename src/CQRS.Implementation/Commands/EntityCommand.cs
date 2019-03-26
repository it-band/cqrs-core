namespace CQRS.Implementation.Commands
{
    public abstract class EntityCommand<TOut, TId> : Implementation.Commands.CommandBase<TOut>
    {
        public TId Id { get; set; }
    }
}
