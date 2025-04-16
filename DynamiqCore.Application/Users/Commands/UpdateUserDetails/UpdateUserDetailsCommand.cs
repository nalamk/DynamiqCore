using MediatR;

namespace DynamiqCore.Application.Users.Commands;

public class UpdateUserDetailsCommand: IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}