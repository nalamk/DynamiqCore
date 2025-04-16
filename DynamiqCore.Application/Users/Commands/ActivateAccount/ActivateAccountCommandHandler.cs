using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.ActivateAccount
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result<string>>
    {
        #region Fields

        private readonly ILogger<ActivateAccountCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor

        public ActivateAccountCommandHandler(
            ILogger<ActivateAccountCommandHandler> logger,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        #endregion


        #region Methods

        public async Task<Result<string>> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Activate account: {@Request}", request);

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) 
            {
                return Result<string>.NotFound($"User with id: {request.UserId} not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if(result.Succeeded)
            {
                _logger.LogInformation($"Account activated successfully: {user.UserName}");
                return Result<string>.Success("Account activated successfully");
            }
            else
            {
                _logger.LogError($"Failed to activate account: {user.UserName}");
                return Result<string>.InternalServerError("Failed to activate account");
            }
        }

        #endregion
    }
}
