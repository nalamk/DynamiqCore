using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamiqCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateOnlyTheDateOfBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOnly",
                table: "AspNetUsers",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "AspNetUsers",
                newName: "DateOnly");
        }
    }
}
