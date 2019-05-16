using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Models;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class CommandHandlerBase<TIn, TOut> : ICommandHandler<TIn, Task<Result<TOut>>>
        where TIn : ICommand<Task<Result<TOut>>>
    {
        public abstract Task<Result<TOut>> Handle(TIn input);
    }
}
