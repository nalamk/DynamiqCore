using FluentValidation;

namespace DynamiqCore.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 100);
    }
}