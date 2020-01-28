using System;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Implementation;
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
            _container.RegisterSingleton<IHandlerDispatcher, HandlerDispatcher>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public async Task HandlerDispatcherDynamicDispatchingSuccess()
        {
            var handlerDispatcher = _container.GetInstance<IHandlerDispatcher>();
            var commandType = typeof(TestCommand);
            var result = await handlerDispatcher.Handle(commandType, new TestCommand());
            Assert.True((bool)result.Data);
        }

        [Fact]
        public async Task HandlerDispatcher_CommandWithVoidResult_DispatchingSuccess()
        {
            var handlerDispatcher = _container.GetInstance<IHandlerDispatcher>();
            var result = await handlerDispatcher.Handle(new TestCommandWithVoidResult());
            Assert.True(result.IsSuccess);
        }
    }
}
