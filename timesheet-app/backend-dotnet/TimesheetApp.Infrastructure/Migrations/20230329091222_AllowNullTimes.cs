using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Start",
                table: "Registration",
                type: "Time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "Time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "End",
                table: "Registration",
                type: "Time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "Time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Start",
                table: "Registration",
                type: "Time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "Time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "End",
                table: "Registration",
                type: "Time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "Time",
                oldNullable: true);
        }
    }
}