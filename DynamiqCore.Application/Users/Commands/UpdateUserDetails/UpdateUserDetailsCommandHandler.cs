using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands;

public class UpdateUserDetailsCommandHandler(
    ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<ApplicationUser> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var applicationUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        if (applicationUser is null)
        {
            throw new NotFoundException(nameof(applicationUser), user!.Id);
        }

        applicationUser.Nationality = request.Nationality;
        applicationUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(applicationUser, cancellationToken);
    }
}