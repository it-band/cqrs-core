namespace CQRS.Models
{
    public interface IDecorator<out T>
    {
        T Decorated { get; }
    }
}
