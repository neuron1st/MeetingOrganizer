using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetingOrganizer.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLikesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_likes_users_UserId",
                table: "comment_likes");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_UserId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_meeting_likes_users_UserId",
                table: "meeting_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meeting_likes",
                table: "meeting_likes");

            migrationBuilder.DropIndex(
                name: "IX_meeting_likes_MeetingId",
                table: "meeting_likes");

            migrationBuilder.DropIndex(
                name: "IX_meeting_likes_Uid",
                table: "meeting_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment_likes",
                table: "comment_likes");

            migrationBuilder.DropIndex(
                name: "IX_comment_likes_Uid",
                table: "comment_likes");

            migrationBuilder.DropIndex(
                name: "IX_comment_likes_UserId",
                table: "comment_likes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "meeting_likes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "meeting_likes");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "meeting_likes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "comment_likes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "comment_likes");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "comment_likes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "meeting_likes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "comment_likes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_meeting_likes",
                table: "meeting_likes",
                columns: new[] { "MeetingId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment_likes",
                table: "comment_likes",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.CreateIndex(
                name: "IX_meeting_likes_MeetingId_UserId",
                table: "meeting_likes",
                columns: new[] { "MeetingId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_comment_likes_UserId_CommentId",
                table: "comment_likes",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_comment_likes_users_UserId",
                table: "comment_likes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "EntryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_UserId",
                table: "comments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "EntryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_meeting_likes_users_UserId",
                table: "meeting_likes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "EntryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_likes_users_UserId",
                table: "comment_likes");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_UserId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_meeting_likes_users_UserId",
                table: "meeting_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meeting_likes",
                table: "meeting_likes");

            migrationBuilder.DropIndex(
                name: "IX_meeting_likes_MeetingId_UserId",
                table: "meeting_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment_likes",
                table: "comment_likes");

            migrationBuilder.DropIndex(
                name: "IX_comment_likes_UserId_CommentId",
                table: "comment_likes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "meeting_likes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "meeting_likes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "meeting_likes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Uid",
                table: "meeting_likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "comment_likes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "comment_likes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "comment_likes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Uid",
                table: "comment_likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_meeting_likes",
                table: "meeting_likes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment_likes",
                table: "comment_likes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_meeting_likes_MeetingId",
                table: "meeting_likes",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_meeting_likes_Uid",
                table: "meeting_likes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comment_likes_Uid",
                table: "comment_likes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comment_likes_UserId",
                table: "comment_likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_likes_users_UserId",
                table: "comment_likes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_UserId",
                table: "comments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_meeting_likes_users_UserId",
                table: "meeting_likes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "EntryId");
        }
    }
}
