using System.Threading.Tasks;
using CQRS.Abstractions.Models;
using CQRS.Models;

namespace CQRS.Implementation.Commands
{
    public class CommandBase<TOut> : ICommand<Task<Result<TOut>>>
    {
    }

    public class CommandBase : ICommand<Task<Result>>
    {

    }
}
