using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class EmployeeHasAuth0Id : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Auth0Id",
            table: "Employee",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Auth0Id",
            table: "Employee");
    }
}
