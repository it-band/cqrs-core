using CQRS.Abstractions.Models;
using Hangfire;

namespace CQRS.Implementation
{
    /// <summary>
    /// Event Publisher which is based on Hangfire infrastructure (https://www.hangfire.io/)
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            BackgroundJob.Enqueue<IEventHandlerDispatcher>(dispatcher => dispatcher.Handle(@event));
        }
    }
}
