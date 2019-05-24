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
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public async Task HandlerDispatcherDynamicDispatchingSuccess()
        {
            var handlerDispatcher = new HandlerDispatcher(_container);
            var commandType = typeof(TestCommand);
            var result = await handlerDispatcher.Handle(commandType, new TestCommand());
            Assert.True((bool)result.Data);
        }
    }
}
