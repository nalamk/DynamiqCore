using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamiqCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeysName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_OrganizationID",
                table: "Patients",
                newName: "IX_Patients_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "OrganizationID",
                table: "Organizations",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "LookupConfigurationID",
                table: "LookupConfigurations",
                newName: "LookupConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Organizations_OrganizationId",
                table: "Patients",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Organizations_OrganizationId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Patients",
                newName: "OrganizationID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_OrganizationId",
                table: "Patients",
                newName: "IX_Patients_OrganizationID");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Organizations",
                newName: "OrganizationID");

            migrationBuilder.RenameColumn(
                name: "LookupConfigurationId",
                table: "LookupConfigurations",
                newName: "LookupConfigurationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Organizations_OrganizationID",
                table: "Patients",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
