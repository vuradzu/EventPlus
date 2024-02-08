using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventPlus.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuthData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CommandPermission",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1L, "mt.cmd+" },
                    { 2L, "mt.cmd.usr+" },
                    { 3L, "mt.evt+" }
                });

            migrationBuilder.InsertData(
                table: "CommandRole",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1L, "Admin" });

            migrationBuilder.InsertData(
                table: "CommandRolePermission",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 2L, 1L },
                    { 3L, 3L, 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommandRolePermission",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CommandRolePermission",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CommandRolePermission",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CommandPermission",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CommandRole",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
