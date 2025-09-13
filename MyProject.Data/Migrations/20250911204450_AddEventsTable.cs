using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Calendars_CalendarId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ClientReferenceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RecurrenceRule",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Events",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "CalendarId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Calendars_CalendarId",
                table: "Events",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Calendars_CalendarId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Events",
                newName: "Title");

            migrationBuilder.AlterColumn<int>(
                name: "CalendarId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientReferenceId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "End",
                table: "Events",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "RecurrenceRule",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Start",
                table: "Events",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Calendars_CalendarId",
                table: "Events",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
