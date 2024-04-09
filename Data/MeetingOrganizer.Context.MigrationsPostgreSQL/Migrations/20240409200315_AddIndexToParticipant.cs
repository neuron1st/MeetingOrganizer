using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingOrganizer.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_participants_MeetingId",
                table: "participants");

            migrationBuilder.CreateIndex(
                name: "IX_participants_MeetingId_UserId",
                table: "participants",
                columns: new[] { "MeetingId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_participants_MeetingId_UserId",
                table: "participants");

            migrationBuilder.CreateIndex(
                name: "IX_participants_MeetingId",
                table: "participants",
                column: "MeetingId");
        }
    }
}
