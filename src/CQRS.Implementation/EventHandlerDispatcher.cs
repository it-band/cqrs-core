using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using SimpleInjector;

namespace CQRS.Implementation
{
    public class EventHandlerDispatcher : IEventHandlerDispatcher
    {
        private readonly Container _container;

        public EventHandlerDispatcher(Container container)
        {
            _container = container;
        }

        public virtual async Task Handle<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var eventHandlers = _container.GetAllInstances<IEventHandler<TEvent>>();

            var eventHandlersTasks = new List<Task>();

            foreach (var eventHandler in eventHandlers)
            {
                eventHandlersTasks.Add(eventHandler.Handle(@event));
            }

            await Task.WhenAll(eventHandlersTasks);
        }
    }
}
