using CQRS.Implementation.Commands;
using FluentValidation;

namespace CQRS.Implementation.Validators
{
    public class PublicEntityCommandValidator<TPublicEntityCommand, TOut, TPublicId> : AbstractValidator<TPublicEntityCommand>
        where TPublicEntityCommand : PublicEntityCommand<TOut, TPublicId>
    {
        public PublicEntityCommandValidator()
        {
            RuleFor(x => x.PublicId).NotEmpty();
        }
    }
}
