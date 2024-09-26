using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TempStart",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TempEnd",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("UPDATE Registration SET TempStart = CONCAT(date, ' ', [start])");
            migrationBuilder.Sql("UPDATE Registration SET TempEnd = CONCAT(date, ' ', [end])");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(TimeSpan),
                oldType: "Time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "End",
                table: "Registration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(TimeSpan),
                oldType: "Time",
                oldNullable: true);

            migrationBuilder.Sql("UPDATE Registration SET [start] = TempStart");
            migrationBuilder.Sql("UPDATE Registration SET [end] = TempEnd");

            migrationBuilder.DropColumn(
                name: "TempStart",
                table: "Registration");
            migrationBuilder.DropColumn(
                name: "TempEnd",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Registration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Registration",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("UPDATE Registration SET Date = CONVERT(date, [start])");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Start",
                table: "Registration",
                type: "Time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "End",
                table: "Registration",
                type: "Time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.Sql("UPDATE Registration SET [Start] = null WHERE [Start] = '00:00:00'");
            migrationBuilder.Sql("UPDATE Registration SET [End] = null WHERE [End] = '00:00:00'");
        }
    }
}