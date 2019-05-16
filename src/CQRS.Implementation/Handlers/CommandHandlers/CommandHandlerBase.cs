using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Implementation.Commands;
using CQRS.Models;

namespace CQRS.Implementation.Handlers.CommandHandlers
{
    public abstract class CommandHandlerBase<TIn, TOut> : ICommandHandler<TIn, Task<Result<TOut>>>
        where TIn : CommandBase<TOut>
    {
        public abstract Task<Result<TOut>> Handle(TIn input);
    }
}
