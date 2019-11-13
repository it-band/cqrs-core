using System;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;
using Microsoft.AspNetCore.Hosting;

namespace CQRS.Implementation.Decorators
{
    public class ErrorHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly AppLogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="logger"></param>
        /// <param name="hostingEnvironment"></param>
        public ErrorHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated, AppLogger logger, IHostingEnvironment hostingEnvironment) : base(decorated)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Decorated Handle
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<Result<TOut>> Handle(TIn input)
        {
            try
            {
                return await Decorated.Handle(input);
            }
            catch (Exception ex)
            {
                var errorId = _logger.Error(ex);

                return _hostingEnvironment.IsDevelopment()
                    ? new ExceptionFailure(ex)
                    : new ExceptionFailure($"An error occurred while processing your request. ErrorId: {errorId}");
            }
        }
    }
}
