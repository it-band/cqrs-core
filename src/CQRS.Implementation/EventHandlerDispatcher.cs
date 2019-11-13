using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
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

        public async Task Handle<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var eventHandlers = _container.GetTypesToRegister<IEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers)
            {
                _backgroundJobClient.Create(new Job(eventHandler, eventHandler.GetMethod("Handle"), @event), new EnqueuedState());
            }

            await Task.CompletedTask;
        }
    }
}
