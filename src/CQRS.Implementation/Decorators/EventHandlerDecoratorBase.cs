using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;

namespace CQRS.Implementation.Decorators
{
    public abstract class EventHandlerDecoratorBase<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        protected readonly IEventHandler<TEvent> Decorated;

        protected EventHandlerDecoratorBase(IEventHandler<TEvent> decorated)
        {
            Decorated = decorated;
        }

        public abstract Task Handle(TEvent @event);
    }
}
