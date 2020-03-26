using System.Threading.Tasks;
using CQRS.Implementation.Commands;
using CQRS.Implementation.Handlers.CommandHandlers;
using CQRS.Models;
using FluentValidation;

namespace CQRS.IntegrationTests.HandlerDispatcherTests
{
    public class TestCommand : CommandBase<bool>
    {
        public string Info { get; set; }
    }

    public class TestCommandValidator : AbstractValidator<TestCommand>
    {
        public TestCommandValidator()
        {
            RuleFor(x => x.Info).NotEmpty();
        }
    }

    public class TestCommandHandler : CommandHandlerBase<TestCommand, bool>
    {
        public override async Task<Result<bool>> Handle(TestCommand input)
        {
            return await Task.FromResult(true);
        }
    }



    public class TestCommandWithVoidResult : CommandBase
    {
        public string Info { get; set;}
    }

    public class TestCommandWithVoidResultValidator : AbstractValidator<TestCommandWithVoidResult>
    {
        public TestCommandWithVoidResultValidator()
        {
            RuleFor(x => x.Info).NotEmpty();
        }
    }

    public class TestCommandWithVoidResultHandler : CommandHandlerBase<TestCommandWithVoidResult>
    {
        public override async Task<Result> Handle(TestCommandWithVoidResult input)
        {
            return await Task.FromResult(Result.Success());
        }
    }
}
