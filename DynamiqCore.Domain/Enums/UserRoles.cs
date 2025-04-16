namespace DynamiqCore.Domain.Enums;

public enum UserRoles
{
    // System specific Roles
    SystemSuperAdmin=1,
    SystemAdmin=2,
    SystemManager=3,
    SystemSupport=4,
    OrganizationSuperAdmin=5,
    
    // Organization Specific Roles
    OrganizationAdmin=6,
    OrganizationDoctor=7,
    OrganizationNurse=8,
    
    // Patient Specific Roles
    Patient=9
}