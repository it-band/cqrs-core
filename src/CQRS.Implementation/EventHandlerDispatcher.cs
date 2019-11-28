using System.Linq;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Implementation.Decorators;
using CQRS.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using SimpleInjector;

namespace CQRS.Implementation
{
    public class EventHandlerDispatcher : IEventHandlerDispatcher
    {
        private readonly Container _container;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public EventHandlerDispatcher(Container container, IBackgroundJobClient backgroundJobClient)
        {
            _container = container;
            _backgroundJobClient = backgroundJobClient;
        }


        public virtual async Task Handle<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var eventHandlers = _container.GetAllInstances<IEventHandler<TEvent>>();
            
            foreach (var eventHandler in eventHandlers)
            {
                var eventHandlerType = eventHandler.GetType();

                if (eventHandler is IDecorator<IEventHandler<TEvent>> decorator)
                {
                    eventHandlerType = decorator.DecoratorsChain().Last().Decorated.GetType();
                }

                _backgroundJobClient.Create(new Job(eventHandlerType, eventHandlerType.GetMethod("Handle"), @event), new EnqueuedState(@event.QueueName));
            }

            await Task.CompletedTask;
        }
    }
}
