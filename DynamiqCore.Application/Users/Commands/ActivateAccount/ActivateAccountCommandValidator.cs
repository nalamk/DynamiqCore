using FluentValidation;

namespace DynamiqCore.Application.Users.Commands.ActivateAccount
{
    public class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
    {
        public ActivateAccountCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
