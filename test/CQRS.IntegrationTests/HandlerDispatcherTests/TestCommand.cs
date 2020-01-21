using System.Threading.Tasks;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Handlers.CommandHandlers;
using CQRS.Models;

namespace CQRS.IntegrationTests.HandlerDispatcherTests
{
    public class TestCommand : CommandBase<object>
    {
    }

    public class TestCommandHandler : CommandHandlerBase<TestCommand, object>
    {
        public override async Task<Result<object>> Handle(TestCommand input)
        {
            return await Task.FromResult(true);
        }
    }

    public class TestCommandWithVoidResult : CommandBase
    {

    }

    public class TestCommandWithVoidResultHandler : CommandHandlerBase<TestCommandWithVoidResult>
    {
        public override async Task<Result> Handle(TestCommandWithVoidResult input)
        {
            return await Task.FromResult(Result.Success());
        }
    }
}
