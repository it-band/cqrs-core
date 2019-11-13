using System;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Models;

namespace CQRS.Implementation.Decorators
{
    public class ErrorEventHandlerDecorator<TEvent> : EventHandlerDecoratorBase<TEvent>
        where TEvent : IEvent
    {
        private readonly AppLogger _logger;

        public ErrorEventHandlerDecorator(IEventHandler<TEvent> decorated, AppLogger logger) : base(decorated)
        {
            _logger = logger;
        }

        /// <summary>
        /// Decorated Handle 
        /// </summary>
        public override async Task Handle(TEvent @event)
        {
            try
            {
                await Decorated.Handle(@event);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
    }
}
