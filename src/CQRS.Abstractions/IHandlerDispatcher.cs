namespace CQRS.Abstractions
{
    public interface IHandlerDispatcher
    {
        TOut Handle<TIn, TOut>(TIn input);
    }
}
