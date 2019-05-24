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

        public async Task<Result<TOut>> Handle<TIn, TOut>(TIn input)
        {
            if (input == null)
            {
                throw new NullReferenceException(typeof(TIn).Name);
            }

            var handler = (IHandler<TIn, Task<Result<TOut>>>)_container.GetInstance(typeof(IHandler<TIn, Task<Result<TOut>>>));

            return await handler.Handle(input);
        }

        public async Task<Result<object>> Handle(Type In, Type Out, object input)
        {
            if (input == null)
            {
                throw new NullReferenceException(In.Name);
            }

            var outType = typeof(Result<>).MakeGenericType(Out);

            var handlerType = typeof(IHandler<,>).MakeGenericType(In, outType);

            var handler = _container.GetInstance(handlerType);

            var handleMethod = handlerType.GetMethod("Handle");

            return await (Task<Result<object>>)handleMethod.Invoke(handler, new[] { input });
        }
    }
}
