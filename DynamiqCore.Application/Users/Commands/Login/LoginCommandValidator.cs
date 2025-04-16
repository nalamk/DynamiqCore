using FluentValidation;

namespace DynamiqCore.Application.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.RememberMe).NotNull();
        }
        
    }
}
