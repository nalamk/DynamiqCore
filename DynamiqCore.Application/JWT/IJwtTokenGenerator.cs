using DynamiqCore.Domain.Entities;

namespace DynamiqCore.Application.JWT
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
