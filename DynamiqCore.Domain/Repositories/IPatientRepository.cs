using DynamiqCore.Domain.Constants;
using DynamiqCore.Domain.Entities;

namespace DynamiqCore.Domain.Repositories;

public interface IPatientRepository
{
    Task<Guid> Create(Patient entity);
    Task Delete(Patient entity);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient> GetByPatientIdIdAsync(Guid? organizationId);
    Task SaveChanges();
    Task<(IEnumerable<Patient>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}