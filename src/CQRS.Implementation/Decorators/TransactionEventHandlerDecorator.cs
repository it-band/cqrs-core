using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Implementation.Transactions;
using SimpleInjector;

namespace CQRS.Implementation.Decorators
{
    public class TransactionEventHandlerDecorator<TEvent> : EventHandlerDecoratorBase<TEvent>
        where TEvent : IEvent
    {
        private readonly IsolationLevel? _transactionType;
        private readonly ITransactionAccessor _transactionAccessor;

        public TransactionEventHandlerDecorator(IEventHandler<TEvent> decorated, ITransactionAccessor transactionAccessor, DecoratorContext decoratorContext) : base(decorated)
        {
            _transactionType = decoratorContext.ImplementationType.GetCustomAttribute<TransactionAttribute>()?.TransactionType;
            _transactionAccessor = transactionAccessor;
        }

        public override async Task Handle(TEvent @event)
        {
            if (_transactionType.HasValue)
            {
                using (var transaction = await _transactionAccessor.BeginTransaction(_transactionType.Value))
                {
                    await Decorated.Handle(@event);

                    transaction.Commit();
                }
            }
            else
            {
                await Decorated.Handle(@event);
            }            
        }
    }
}
