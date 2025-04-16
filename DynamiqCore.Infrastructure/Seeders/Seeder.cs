using DynamiqCore.Domain.Constants;
using DynamiqCore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace DynamiqCore.Infrastructure.Seeders;

internal class Seeder(DynamiqCoreDbContext dbContext) : ISeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                await dbContext.Roles.AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }


    #region Helpers

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = 
        [
            new (UserRoles.SystemAdmin)
            {
                NormalizedName = UserRoles.SystemAdmin.ToUpper()
            },
            new (UserRoles.SystemManager)
            {
                NormalizedName = UserRoles.SystemManager.ToUpper()
            },
            new IdentityRole(UserRoles.OrganizationAdmin)
            {
                NormalizedName = UserRoles.OrganizationAdmin.ToUpper()
            },
            new (UserRoles.OrganizationAdmin)
            {
                NormalizedName = UserRoles.OrganizationAdmin.ToUpper()
            },
            new (UserRoles.Patient)
            {
            NormalizedName = UserRoles.Patient.ToUpper()
            }
        ];

        return roles;
    }

    #endregion
}