using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usermanagement_API.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Permissions_permissionID",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "permissionID",
                table: "UserPermissions",
                newName: "permissionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_permissionID",
                table: "UserPermissions",
                newName: "IX_UserPermissions_permissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Permissions_permissionId",
                table: "UserPermissions",
                column: "permissionId",
                principalTable: "Permissions",
                principalColumn: "permissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Permissions_permissionId",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "permissionId",
                table: "UserPermissions",
                newName: "permissionID");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_permissionId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_permissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Permissions_permissionID",
                table: "UserPermissions",
                column: "permissionID",
                principalTable: "Permissions",
                principalColumn: "permissionId");
        }
    }
}
