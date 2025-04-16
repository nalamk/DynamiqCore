using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamiqCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Organizations_ParentOrganizationId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ParentOrganizationId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ParentOrganizationId",
                table: "Organizations");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentOrganizationId",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ParentOrganizationId",
                table: "Organizations",
                column: "ParentOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Organizations_ParentOrganizationId",
                table: "Organizations",
                column: "ParentOrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
