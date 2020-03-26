using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Models;
using FluentValidation;

namespace CQRS.Implementation.Decorators
{
    public class ValidationHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IEnumerable<IValidator<TIn>> _validators;

        public ValidationHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated, IEnumerable<IValidator<TIn>> validators) : base(decorated)
        {
            _validators = validators;
        }

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            var context = new ValidationContext(input);

            var failures = _validators
                .Select(async v => await v.ValidateAsync(context))
                .SelectMany(result => result.Result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                return Result.ValidationError(failures.Select(f => new ValidationError {Field = f.PropertyName, Message = f.ErrorMessage}).ToArray());
            }

            return await Decorated.Handle(input);
        }
    }

    public class ValidationHandlerDecorator<TIn> : HandlerDecoratorBase<TIn>
    {
        private readonly IEnumerable<IValidator<TIn>> _validators;

        public ValidationHandlerDecorator(IHandler<TIn, Task<Result>> decorated, IEnumerable<IValidator<TIn>> validators) : base(decorated)
        {
            _validators = validators;
        }

        public override async Task<Result> Handle(TIn input)
        {
            var context = new ValidationContext(input);

            var failures = _validators
                .Select(async v => await v.ValidateAsync(context))
                .SelectMany(result => result.Result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                return Result.ValidationError(failures.Select(f => new ValidationError { Field = f.PropertyName, Message = f.ErrorMessage }).ToArray());
            }

            return await Decorated.Handle(input);
        }
    }
}
