using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class URID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RUID",
                table: "tbl_UserRole",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_UserRole",
                table: "tbl_UserRole",
                column: "RUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_UserRole",
                table: "tbl_UserRole");

            migrationBuilder.DropColumn(
                name: "RUID",
                table: "tbl_UserRole");
        }
    }
}
