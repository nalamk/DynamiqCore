using DynamiqCore.Domain.Constants;
using DynamiqCore.Domain.Entities;

namespace DynamiqCore.Domain.Interfaces;

public interface IOrganizationAuthorizationService
{
    bool Authorize(Organization organization, ResourceOperation resourceOperation);
}