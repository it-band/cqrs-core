using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Models;

namespace CQRS.Implementation.Decorators
{
    public abstract class EventHandlerDecoratorBase<TEvent> : IEventHandler<TEvent>, IDecorator<IEventHandler<TEvent>>
        where TEvent : IEvent
    {
        public IEventHandler<TEvent> Decorated { get; }

        protected EventHandlerDecoratorBase(IEventHandler<TEvent> decorated)
        {
            Decorated = decorated;
        }

        public abstract Task Handle(TEvent @event);        
    }
}
