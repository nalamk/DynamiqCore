using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamiqCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrganizationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OrganizationID",
                table: "AspNetUsers",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_OrganizationID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "AspNetUsers",
                newName: "OrganizationID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_OrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationID",
                table: "AspNetUsers",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
