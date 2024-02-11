using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlus.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommandPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandMemberPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandMemberPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandMemberPermission_CommandMember_MemberId",
                        column: x => x.MemberId,
                        principalTable: "CommandMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandMemberPermission_CommandPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "CommandPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandMemberRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandMemberRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandMemberRole_CommandMember_MemberId",
                        column: x => x.MemberId,
                        principalTable: "CommandMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandMemberRole_CommandRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CommandRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandRolePermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandRolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandRolePermission_CommandPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "CommandPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandRolePermission_CommandRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CommandRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandMemberPermission_MemberId",
                table: "CommandMemberPermission",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandMemberPermission_PermissionId",
                table: "CommandMemberPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandMemberRole_MemberId",
                table: "CommandMemberRole",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandMemberRole_RoleId",
                table: "CommandMemberRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandRolePermission_PermissionId",
                table: "CommandRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandRolePermission_RoleId",
                table: "CommandRolePermission",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandMemberPermission");

            migrationBuilder.DropTable(
                name: "CommandMemberRole");

            migrationBuilder.DropTable(
                name: "CommandRolePermission");

            migrationBuilder.DropTable(
                name: "CommandPermission");

            migrationBuilder.DropTable(
                name: "CommandRole");
        }
    }
}
