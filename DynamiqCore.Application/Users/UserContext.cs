using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DynamiqCore.Application.Users;

public class UserContext: IUserContext
{

    #region Fields

    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructors

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    #endregion

    #region IUserContext Implementation

    public CurrentUser? GetCurrentUser()
    {
        var user = _httpContextAccessor?.HttpContext?.User;

        if (user is null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);

        return new CurrentUser(userId, email, roles);
    }

    #endregion

}