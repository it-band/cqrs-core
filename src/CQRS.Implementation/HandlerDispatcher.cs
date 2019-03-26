using System;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;

namespace CQRS.Implementation
{
    public class HandlerDispatcher : IHandlerDispatcher
    {
        private readonly IServiceProvider _provider;

        public HandlerDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<Result<TOut>> Handle<TIn, TOut>(TIn input)
        {
            var handler = (IHandler<TIn, Task<Result<TOut>>>) _provider.GetService(typeof(IHandler<TIn, Task<Result<TOut>>>));

            return await handler.Handle(input);
        }         
    }
}
