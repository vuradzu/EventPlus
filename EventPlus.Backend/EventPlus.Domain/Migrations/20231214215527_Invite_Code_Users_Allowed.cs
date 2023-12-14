using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlus.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Invite_Code_Users_Allowed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaximumUses",
                table: "InviteCode",
                newName: "UsersAllowed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsersAllowed",
                table: "InviteCode",
                newName: "MaximumUses");
        }
    }
}
