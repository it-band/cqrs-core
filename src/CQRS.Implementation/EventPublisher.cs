using CQRS.Abstractions.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;

namespace CQRS.Implementation
{
    /// <summary>
    /// Event Publisher which is based on Hangfire infrastructure (https://www.hangfire.io/)
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public EventPublisher(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public virtual void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var method = typeof(IEventHandlerDispatcher).GetMethod("Handle")?.MakeGenericMethod(typeof(TEvent));
            
            _backgroundJobClient.Create(new Job(method, @event), new EnqueuedState(@event.QueueName));
        }
    }
}
