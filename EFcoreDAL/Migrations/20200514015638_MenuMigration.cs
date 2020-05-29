using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class MenuMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Menu",
                columns: table => new
                {
                    MenuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Menu", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserMenu",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: true),
                    MenuID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Menu");

            migrationBuilder.DropTable(
                name: "tbl_UserMenu");
        }
    }
}
