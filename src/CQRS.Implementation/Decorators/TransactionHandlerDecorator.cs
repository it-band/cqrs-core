using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Implementation.Transactions;
using CQRS.Models;
using SimpleInjector;

namespace CQRS.Implementation.Decorators
{
    public class TransactionHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        private readonly IsolationLevel _transactionType;

        public TransactionHandlerDecorator(DecoratorContext decoratorContext, IHandler<TIn, Task<Result<TOut>>> decorated) : base(decorated)
        {
            _transactionType = decoratorContext.ImplementationType
                .GetCustomAttribute<TransactionAttribute>()
                .TransactionType;
        }        

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = _transactionType }))
            {
                var result = await Decorated.Handle(input);

                scope.Complete();

                return result;
            }
        }
    }
}
