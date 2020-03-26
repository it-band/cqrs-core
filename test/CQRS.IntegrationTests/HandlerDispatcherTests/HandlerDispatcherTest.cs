using System;
using System.Data;
using System.Threading.Tasks;
using Castle.Core.Logging;
using CQRS.Abstractions;
using CQRS.Implementation;
using CQRS.Implementation.Decorators;
using CQRS.Implementation.Models;
using CQRS.Implementation.Transactions;
using CQRS.Models;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleInjector;
using Xunit;

namespace CQRS.IntegrationTests.HandlerDispatcherTests
{
    public class HandlerDispatcherTest : IDisposable
    {
        private readonly Container _container;
        public HandlerDispatcherTest()
        {
            _container = new Container();
            _container.Register(typeof(IHandler<,>), GetType().Assembly);
            _container.Collection.Register(typeof(IValidator<>), GetType().Assembly);
            _container.Collection.Register(typeof(IPermissionValidator<>), GetType().Assembly);

            var transactionAccessorMoq = new Mock<ITransactionAccessor>();

            var dbTransactionMoq = new Mock<IDbContextTransaction>();

            transactionAccessorMoq.Setup(a => a.BeginTransaction(It.IsAny<IsolationLevel>())).ReturnsAsync(dbTransactionMoq.Object);

            _container.RegisterInstance(new Mock<AppLogger>(new Mock<ILogger<AppLogger>>().Object).Object);

            _container.RegisterInstance(transactionAccessorMoq.Object);

            _container.RegisterInstance(new Mock<IHostingEnvironment>().Object);

            _container.RegisterSingleton<IHandlerDispatcher, HandlerDispatcher>();

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(TransactionHandlerDecorator<,>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(TransactionHandlerDecorator<>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(ValidationHandlerDecorator<,>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(ValidationHandlerDecorator<>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(PermissionValidationHandlerDecorator<,>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(PermissionValidationHandlerDecorator<>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(ErrorHandlerDecorator<,>));

            _container.RegisterDecorator(typeof(IHandler<,>), typeof(ErrorHandlerDecorator<>));
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public async Task Command_Dispatching_Success()
        {
            var handlerDispatcher = _container.GetInstance<IHandlerDispatcher>();
            var result = await handlerDispatcher.Handle<TestCommand, bool>(new TestCommand
            {
                Info = "Test info"
            });

            Assert.True(result.Data);
        }

        [Fact]
        public async Task Command_Dispatching_ValidationError()
        {
            var handlerDispatcher = _container.GetInstance<IHandlerDispatcher>();
            var result = await handlerDispatcher.Handle<TestCommand, bool>(new TestCommand());
            Assert.False(result.IsSuccess);
            Assert.True(result.Failure is ValidationFailure);
        }

        [Fact]
        public async Task CommandWithVoidResult_Dispatching_Success()
        {
            var handlerDispatcher = _container.GetInstance<IHandlerDispatcher>();
            var result = await handlerDispatcher.Handle(new TestCommandWithVoidResult {Info = "Test info"});
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async Task CommandWithVoidResult_Dispatching_ValidationError()
        {
            var handlerDispatcher = _container.GetInstance<IHandlerDispatcher>();
            var result = await handlerDispatcher.Handle(new TestCommandWithVoidResult());
            Assert.False(result.IsSuccess);
            Assert.True(result.Failure is ValidationFailure);
        }

    }
}
