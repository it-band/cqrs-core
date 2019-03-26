using System;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;
using SimpleInjector;

namespace CQRS.Implementation
{
    public class HandlerDispatcher : IHandlerDispatcher
    {
        protected readonly Container Container;

        public HandlerDispatcher(Container container)
        {
            Container = container;
        }

        public async Task<Result<TOut>> Handle<TIn, TOut>(TIn input)
        {
            if (input == null)
            {
                throw new NullReferenceException(typeof(TIn).Name);
            }

            var handler = (IHandler<TIn, Task<Result<TOut>>>)Container.GetInstance(typeof(IHandler<TIn, Task<Result<TOut>>>));

            return await handler.Handle(input);
        }
    }
}
