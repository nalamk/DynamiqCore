using AutoMapper;
using DynamiqCore.Application.Users.Commands.PatientSignup.Dtos;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Interfaces;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.PatientSignup;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<CreatePatientDto>>
{
    #region Fields

    private readonly ILogger<CreatePatientCommandHandler> _logger;
    private readonly IPatientRepository _patientRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    #endregion

    #region Constructor

    public CreatePatientCommandHandler(
        ILogger<CreatePatientCommandHandler> logger,
        IPatientRepository patientRepository,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #endregion

    #region Handler

    public async Task<Result<CreatePatientDto>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            
            // If OrganizationId is empty return Organization Notfound
            if (!request.OrganizationId.HasValue || request.OrganizationId.Value == Guid.Empty)
            {
                return Result<CreatePatientDto>.NotFound("OrganizationId can not be empty.");
            }
            
            // Create the new user
            var newUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                OrganizationId = request.OrganizationId,
                EmailConfirmed = true
            };
            
            var creationResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description).ToArray();
                return Result<CreatePatientDto>.BadRequest(errors);
            }
            
            // Add patient
            var newPatient = _mapper.Map<Patient>(request);
            var patientId = await _patientRepository.Create(newPatient);

            
            var signupDto = new CreatePatientDto
            {
                Message = "Confirmation email sent. Please verify your email.",
                Email = newUser.Email,
                Username = request.Username,
                PatientId = patientId,
                OrganizationId = request.OrganizationId.Value
            };

            // Return success with a message that the email has been sent
            return Result<CreatePatientDto>.Success(signupDto);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred during user signup");
            return Result<CreatePatientDto>.InternalServerError("An error occurred while processing your request.");
        }
        
    }

    #endregion

}