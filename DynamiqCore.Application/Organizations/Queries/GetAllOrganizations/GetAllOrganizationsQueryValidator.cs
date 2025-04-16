using DynamiqCore.Application.Organizations.Queries.Dtos;
using FluentValidation;

namespace DynamiqCore.Application.Organizations.Queries.GetAllOrganizations;

public class GetAllOrganizationsQueryValidator : AbstractValidator<GetAllOrganizationsQuery>
{
    private int[] allowPageSizes = [5, 10, 15, 30];
    private string[] allowedSortByColumnNames = [nameof(OrganizationDto.Name),
        // nameof(OrganizationDto.Category),
        nameof(OrganizationDto.Description)];
    
    public GetAllOrganizationsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
    
}