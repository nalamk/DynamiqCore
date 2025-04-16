// using DynamiqCore.Application.Users;
// using DynamiqCore.Domain.Enums;
// // using DynamiqCore.Domain.Constants;
// using FluentAssertions;
//
// namespace DynamiqCore.Application.Tests.Users;
//
// public class CurrentUserTests
// {
//     [Theory]
//     [InlineData(UserRoles.OrganizationAdmin)]
//     [InlineData(UserRoles.OrganizationUser)]
//     public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
//     {
//         // Arrange
//         var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.OrganizationAdmin, UserRoles.OrganizationUser]);
//
//         // Act
//         var isInRole = currentUser.IsInRole(roleName);
//
//         // Assert
//         isInRole.Should().BeTrue();
//     }
//     
//     [Fact]
//     public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
//     {
//         // Arrange
//         var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.OrganizationAdmin, UserRoles.OrganizationUser]);
//
//         // Act
//         var isInRole = currentUser.IsInRole(UserRoles.OrganizationAdmin);
//
//         // Assert
//         isInRole.Should().BeFalse();
//     }
//     
//     [Fact]
//     public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
//     {
//         // Arrange
//         var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.OrganizationAdmin, UserRoles.OrganizationUser]);
//
//         // Act
//         var isInRole = currentUser.IsInRole(UserRoles.OrganizationAdmin.ToLower());
//
//         // Assert
//         isInRole.Should().BeFalse();
//     }
// }