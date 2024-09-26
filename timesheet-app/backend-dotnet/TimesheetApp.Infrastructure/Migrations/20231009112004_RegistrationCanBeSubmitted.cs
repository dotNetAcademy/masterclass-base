using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationCanBeSubmitted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Registration",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Registration");
        }
    }
}