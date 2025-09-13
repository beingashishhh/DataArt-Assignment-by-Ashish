using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeetings_AttendeeList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttendeeIdsCsv",
                table: "Meetings",
                newName: "AttendeeIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttendeeIds",
                table: "Meetings",
                newName: "AttendeeIdsCsv");
        }
    }
}
