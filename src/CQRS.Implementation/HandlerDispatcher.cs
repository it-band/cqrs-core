using System;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;
using SimpleInjector;

namespace CQRS.Implementation
{
    public class HandlerDispatcher : IHandlerDispatcher
    {
        private readonly Container _container;

        public HandlerDispatcher(Container container)
        {
            _container = container;
        }

        public async Task<Result> Handle<TIn>(TIn input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var handler = (IHandler<TIn, Task<Result>>)_container.GetInstance(typeof(IHandler<TIn, Task<Result>>));

            return await handler.Handle(input);
        }

        public async Task<Result<TOut>> Handle<TIn, TOut>(TIn input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var handler = (IHandler<TIn, Task<Result<TOut>>>)_container.GetInstance(typeof(IHandler<TIn, Task<Result<TOut>>>));

            return await handler.Handle(input);
        }
    }
}
