using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class RemoveRoleIDFromUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "tbl_User");

            migrationBuilder.CreateTable(
                name: "tbl_Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Category", x => x.CategoryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Category");

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "tbl_User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
