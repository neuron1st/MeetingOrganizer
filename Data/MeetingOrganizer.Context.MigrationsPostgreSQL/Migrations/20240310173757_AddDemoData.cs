using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingOrganizer.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddDemoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "participants");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "meetings");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "meeting_likes");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "comment_likes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "photos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "participants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "meetings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "meeting_likes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "comment_likes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
