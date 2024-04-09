using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetingOrganizer.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoleToEnum : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_participants_roles_RoleId",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_claims_AspNetRoles_RoleId",
                table: "user_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_owners_AspNetRoles_RoleId",
                table: "user_role_owners");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropIndex(
                name: "IX_participants_RoleId",
                table: "participants");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "participants");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "user_roles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "user_roles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "participants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "meeting_likes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "user_roles",
                column: "NormalizedName",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_claims_user_roles_RoleId",
                table: "user_role_claims",
                column: "RoleId",
                principalTable: "user_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_owners_user_roles_RoleId",
                table: "user_role_owners",
                column: "RoleId",
                principalTable: "user_roles",
                principalColumn: "Id",
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

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_claims_user_roles_RoleId",
                table: "user_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_owners_user_roles_RoleId",
                table: "user_role_owners");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "participants");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "user_roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "user_roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_participants_RoleId",
                table: "participants",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_Uid",
                table: "roles",
                column: "Uid",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_participants_roles_RoleId",
                table: "participants",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_claims_AspNetRoles_RoleId",
                table: "user_role_claims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_owners_AspNetRoles_RoleId",
                table: "user_role_owners",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
