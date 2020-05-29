using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class UserRoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_UserRole",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: true),
                    RoleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_UserRole");
        }
    }
}
