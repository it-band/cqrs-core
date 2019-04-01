using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Abstractions;
using CQRS.Abstractions.Models;
using CQRS.Implementation.Models;
using CQRS.Models;

namespace CQRS.Implementation.Decorators
{
    public class PermissionValidationHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        private readonly IEnumerable<IPermissionValidator<TIn>> _permissionValidators;

        public PermissionValidationHandlerDecorator(IHandler<TIn, Task<Result<TOut>>> decorated, IEnumerable<IPermissionValidator<TIn>> permissionValidators) : base(decorated)
        {
            _permissionValidators = permissionValidators;
        }

        public override async Task<Result<TOut>> Handle(TIn input)
        {
            foreach (var permissionValidator in _permissionValidators)
            {
                var result = await permissionValidator.Validate(input);

                if (!result.IsValid)
                {
                    return Result.Forbidden(result.ToString());
                }
            }

            return await Decorated.Handle(input);
        }
    }
}
