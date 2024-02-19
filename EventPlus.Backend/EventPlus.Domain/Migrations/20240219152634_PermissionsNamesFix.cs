using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlus.Domain.Migrations
{
    /// <inheritdoc />
    public partial class PermissionsNamesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Title",
                value: "ca.cmd+");

            migrationBuilder.UpdateData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Title",
                value: "ca.cmd.usr+");

            migrationBuilder.UpdateData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Title",
                value: "ca.evt+");

            migrationBuilder.InsertData(
                table: "CommandPermission",
                columns: new[] { "Id", "Title" },
                values: new object[] { 4L, "ca.cmd" });
            
            migrationBuilder.InsertData(
                table: "CommandRolePermission",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { 4L, 4L, 1L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommandRolePermission",
                keyColumn: "Id",
                keyValue: 4L);
            
            migrationBuilder.DeleteData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Title",
                value: "mt.cmd+");

            migrationBuilder.UpdateData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Title",
                value: "mt.cmd.usr+");

            migrationBuilder.UpdateData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Title",
                value: "mt.evt+");
        }
    }
}
