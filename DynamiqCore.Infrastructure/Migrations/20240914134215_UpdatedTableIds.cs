using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamiqCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Organizations_OrganizationId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Patients",
                newName: "OrganizationID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patients",
                newName: "PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_OrganizationId",
                table: "Patients",
                newName: "IX_Patients_OrganizationID");

            migrationBuilder.RenameColumn(
                name: "LookupConfigurationId",
                table: "LookupConfigurations",
                newName: "LookupConfigurationID");

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
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Organizations_OrganizationID",
                table: "Patients",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Organizations_OrganizationID",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "OrganizationID",
                table: "Patients",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Patients",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_OrganizationID",
                table: "Patients",
                newName: "IX_Patients_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "LookupConfigurationID",
                table: "LookupConfigurations",
                newName: "LookupConfigurationId");

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
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Organizations_OrganizationId",
                table: "Patients",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
