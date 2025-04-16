namespace DynamiqCore.Domain.Constants;

public class UserRoles
{
    // System specific Roles
    public const string SystemSuperAdmin = "SystemSuperAdmin";
    public const string SystemAdmin = "SystemAdmin";
    public const string SystemManager = "SystemManager";
    public const string SystemSupport = "SystemSupport";
    public const string OrganizationSuperAdmin = "OrganizationSuperAdmin";
    
    // Organization Specific Roles
    public const string OrganizationAdmin = "OrganizationAdmin";
    public const string OrganizationDoctor = "OrganizationDoctor";
    public const string OrganizationNurse = "OrganizationNurse";
    
    // Patient Specific Roles
    public const string Patient = "Patient";
}