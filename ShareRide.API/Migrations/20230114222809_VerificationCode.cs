using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.API.Migrations
{
    /// <inheritdoc />
    public partial class VerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_VerificationCode_VerificationCodeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationCode",
                table: "VerificationCode");

            migrationBuilder.RenameTable(
                name: "VerificationCode",
                newName: "VerificationCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationCodes",
                table: "VerificationCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VerificationCodes_VerificationCodeId",
                table: "Users",
                column: "VerificationCodeId",
                principalTable: "VerificationCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_VerificationCodes_VerificationCodeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationCodes",
                table: "VerificationCodes");

            migrationBuilder.RenameTable(
                name: "VerificationCodes",
                newName: "VerificationCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationCode",
                table: "VerificationCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VerificationCode_VerificationCodeId",
                table: "Users",
                column: "VerificationCodeId",
                principalTable: "VerificationCode",
                principalColumn: "Id");
        }
    }
}
