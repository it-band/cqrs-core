using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;
using SimpleInjector;

namespace CQRS.Implementation.Decorators
{
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, Task<Result<TOut>>>
    {
        protected readonly IHandler<TIn, Task<Result<TOut>>> Decorated;

        protected HandlerDecoratorBase(IHandler<TIn, Task<Result<TOut>>> decorated)
        {            
            Decorated = decorated;
        }

        public abstract Task<Result<TOut>> Handle(TIn input);
    }
}
