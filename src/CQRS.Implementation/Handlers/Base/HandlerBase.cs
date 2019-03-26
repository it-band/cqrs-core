using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;

namespace CQRS.Implementation.Handlers.Base
{
    public abstract class HandlerBase<TIn, TOut> : IHandler<TIn, Task<Result<TOut>>>
    {
        public abstract Task<Result<TOut>> Handle(TIn input);
    }
}
