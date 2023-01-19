using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.API.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "VerificationCodeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VerificationCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpire = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCode", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_VerificationCodeId",
                table: "Users",
                column: "VerificationCodeId",
                unique: true,
                filter: "[VerificationCodeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VerificationCode_VerificationCodeId",
                table: "Users",
                column: "VerificationCodeId",
                principalTable: "VerificationCode",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_VerificationCode_VerificationCodeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "VerificationCode");

            migrationBuilder.DropIndex(
                name: "IX_Users_VerificationCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationCodeId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
