using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class MUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MUID",
                table: "tbl_UserMenu",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_UserMenu",
                table: "tbl_UserMenu",
                column: "MUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_UserMenu",
                table: "tbl_UserMenu");

            migrationBuilder.DropColumn(
                name: "MUID",
                table: "tbl_UserMenu");
        }
    }
}
