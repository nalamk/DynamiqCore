using System.Linq.Expressions;
using DynamiqCore.Domain.Constants;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DynamiqCore.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    #region Fields

    private readonly DynamiqCoreDbContext _dbContext;
    
    #endregion

    #region Constructor

    public PatientRepository(DynamiqCoreDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #endregion

    #region IPatientRepository Implementation
    
    public async Task<Guid> Create(Patient entity)
    {
        _dbContext.Patients.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.PatientId;
    }

    public async Task Delete(Patient entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        var patients = await _dbContext.Patients.ToListAsync();
        return patients;
    }

    public async Task<Patient> GetByPatientIdIdAsync(Guid? patientId)
    {
        if (patientId == null)
        {
            return null;
        }

        var patient = await _dbContext.Patients
            .FirstOrDefaultAsync(x => x.PatientId == patientId);

        return patient;
    }

    public Task SaveChanges() => _dbContext.SaveChangesAsync();

    public async Task<(IEnumerable<Patient>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = _dbContext
            .Patients
            .Where(r => searchPhraseLower == null || (r.FirstName.ToLower().Contains(searchPhraseLower))
                                                      || (r.MiddleName.ToLower().Contains(searchPhraseLower))
                                                          || (r.LastName.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Patient, object>>>
            {
                { nameof(Patient.FirstName), r => r.FirstName },
                { nameof(Patient.MiddleName), r => r.MiddleName },
                { nameof(Patient.LastName), r => r.LastName }
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var patients = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (patients, totalCount);
    }
    
    #endregion
}