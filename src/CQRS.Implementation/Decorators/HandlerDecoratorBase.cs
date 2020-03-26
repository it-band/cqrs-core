using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;

namespace CQRS.Implementation.Decorators
{
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, Task<Result<TOut>>>, IDecorator<IHandler<TIn, Task<Result<TOut>>>>
    {
        public IHandler<TIn, Task<Result<TOut>>> Decorated { get; }

        protected HandlerDecoratorBase(IHandler<TIn, Task<Result<TOut>>> decorated)
        {            
            Decorated = decorated;
        }

        public abstract Task<Result<TOut>> Handle(TIn input);
    }

    public abstract class HandlerDecoratorBase<TIn> : IHandler<TIn, Task<Result>>, IDecorator<IHandler<TIn, Task<Result>>>
    {
        public IHandler<TIn, Task<Result>> Decorated { get; }

        protected HandlerDecoratorBase(IHandler<TIn, Task<Result>> decorated)
        {
            Decorated = decorated;
        }

        public abstract Task<Result> Handle(TIn input);
    }
}
