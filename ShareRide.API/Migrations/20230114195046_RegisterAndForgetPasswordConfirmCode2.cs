using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.API.Migrations
{
    /// <inheritdoc />
    public partial class RegisterAndForgetPasswordConfirmCode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePicture_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
