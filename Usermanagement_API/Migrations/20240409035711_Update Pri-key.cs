using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usermanagement_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrikey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Permissions_permissionId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_userId",
                table: "UserPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "UserPermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "permissionId",
                table: "UserPermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Permissions_permissionId",
                table: "UserPermissions",
                column: "permissionId",
                principalTable: "Permissions",
                principalColumn: "permissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_userId",
                table: "UserPermissions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Permissions_permissionId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_userId",
                table: "UserPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "UserPermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "permissionId",
                table: "UserPermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Permissions_permissionId",
                table: "UserPermissions",
                column: "permissionId",
                principalTable: "Permissions",
                principalColumn: "permissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_userId",
                table: "UserPermissions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
