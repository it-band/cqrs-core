using CQRS.Abstractions.Models;

namespace CQRS.Implementation
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}
