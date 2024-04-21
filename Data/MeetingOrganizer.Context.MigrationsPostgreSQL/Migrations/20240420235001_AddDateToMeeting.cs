using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingOrganizer.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "meetings",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "meetings");
        }
    }
}
