using System.Linq.Expressions;
using DynamiqCore.Domain.Constants;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DynamiqCore.Infrastructure.Repositories;

public class OrganizationsRepository : IOrganizationsRepository
{

    #region Fields

    private readonly DynamiqCoreDbContext _dbContext;

    #endregion

    #region Constructor

    public OrganizationsRepository(DynamiqCoreDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #endregion
    
    public async Task<Guid> Create(Organization entity)
    {
        _dbContext.Organizations.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.OrganizationId;
    }
    
    public async Task Delete(Organization entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        var organizations = await _dbContext.Organizations.ToListAsync();
        return organizations;
    }

    public async Task<(IEnumerable<Organization>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = _dbContext
            .Organizations
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                      || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Organization, object>>>
            {
                { nameof(Organization.Name), r => r.Name },
                { nameof(Organization.Description), r => r.Description },
                //{ nameof(Organization.Category), r => r.Category },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        
        var organizations = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (organizations, totalCount);
    }
    
    public async Task<Organization> GetByOrganizationIdAsync(Guid? organizationId)
    {
        var organization = await _dbContext.Organizations
            .FindAsync(organizationId);

        return organization;
    }
    
    public Task SaveChanges() => _dbContext.SaveChangesAsync();
    
}