using System.Threading.Tasks;
using CQRS.Abstractions.Models;

namespace CQRS.Models.Commands
{
    public class CommandBase<TOut> : ICommand<Task<Result<TOut>>>
    {
    }
}
