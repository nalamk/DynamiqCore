using DynamiqCore.Domain.Constants;
using DynamiqCore.Domain.Entities;

namespace DynamiqCore.Domain.Repositories;

public interface IOrganizationsRepository
{
    Task<Guid> Create(Organization entity);
    Task Delete(Organization entity);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<Organization> GetByOrganizationIdAsync(Guid? organizationId);
    Task SaveChanges();
    Task<(IEnumerable<Organization>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}