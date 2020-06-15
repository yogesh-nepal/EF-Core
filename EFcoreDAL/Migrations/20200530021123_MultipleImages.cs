using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class MultipleImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleImageData",
                table: "tbl_MultipleImagePost");

            migrationBuilder.CreateTable(
                name: "tbl_MultipleImageData",
                columns: table => new
                {
                    MultipleImageDataID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePathData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APostWithMultipleImageModelMultipleImagePostID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MultipleImageData", x => x.MultipleImageDataID);
                    table.ForeignKey(
                        name: "FK_tbl_MultipleImageData_tbl_MultipleImagePost_APostWithMultipleImageModelMultipleImagePostID",
                        column: x => x.APostWithMultipleImageModelMultipleImagePostID,
                        principalTable: "tbl_MultipleImagePost",
                        principalColumn: "MultipleImagePostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MultipleImageData_APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData",
                column: "APostWithMultipleImageModelMultipleImagePostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MultipleImageData");

            migrationBuilder.AddColumn<string>(
                name: "MultipleImageData",
                table: "tbl_MultipleImagePost",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
