namespace DynamiqCore.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}