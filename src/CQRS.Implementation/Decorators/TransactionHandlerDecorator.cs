using System.Reflection;
using System.Threading.Tasks;
using System.Data;
using CQRS.Abstractions;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Transactions;
using CQRS.Models;
using SimpleInjector;

namespace CQRS.Implementation.Decorators
{
    public class TransactionHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TIn : CommandBase<TOut>
    {
        private readonly IsolationLevel? _transactionType;
        private readonly ITransactionAccessor _transactionAccessor;

        public TransactionHandlerDecorator(DecoratorContext decoratorContext, IHandler<TIn, Task<Result<TOut>>> decorated, ITransactionAccessor transactionAccessor) : base(decorated)
        {
            _transactionAccessor = transactionAccessor;
            _transactionType = decoratorContext.ImplementationType
                .GetCustomAttribute<TransactionAttribute>()?.TransactionType;
        }        

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            if (_transactionType.HasValue)
            {
                using (var transaction = await _transactionAccessor.BeginTransaction(_transactionType.Value))
                {
                    var result = await Decorated.Handle(input);

                    transaction.Commit();

                    return result;
                }
            }

            return await Decorated.Handle(input);
        }
    }
}
