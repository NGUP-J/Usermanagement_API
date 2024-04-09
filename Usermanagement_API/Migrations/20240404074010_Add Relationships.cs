using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usermanagement_API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "permissionID",
                table: "UserPermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_permissionID",
                table: "UserPermissions",
                column: "permissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Permissions_permissionID",
                table: "UserPermissions",
                column: "permissionID",
                principalTable: "Permissions",
                principalColumn: "permissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Permissions_permissionID",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_permissionID",
                table: "UserPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "permissionID",
                table: "UserPermissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
