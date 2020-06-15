using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class restorevirtuals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_MultipleImageData_tbl_MultipleImagePost_APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData");

            migrationBuilder.DropIndex(
                name: "IX_tbl_MultipleImageData_APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData");

            migrationBuilder.DropColumn(
                name: "APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData");

            migrationBuilder.AddColumn<int>(
                name: "APostMultipleImagePostID",
                table: "tbl_MultipleImageData",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MultipleImageData_APostMultipleImagePostID",
                table: "tbl_MultipleImageData",
                column: "APostMultipleImagePostID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_MultipleImageData_tbl_MultipleImagePost_APostMultipleImagePostID",
                table: "tbl_MultipleImageData",
                column: "APostMultipleImagePostID",
                principalTable: "tbl_MultipleImagePost",
                principalColumn: "MultipleImagePostID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_MultipleImageData_tbl_MultipleImagePost_APostMultipleImagePostID",
                table: "tbl_MultipleImageData");

            migrationBuilder.DropIndex(
                name: "IX_tbl_MultipleImageData_APostMultipleImagePostID",
                table: "tbl_MultipleImageData");

            migrationBuilder.DropColumn(
                name: "APostMultipleImagePostID",
                table: "tbl_MultipleImageData");

            migrationBuilder.AddColumn<int>(
                name: "APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MultipleImageData_APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData",
                column: "APostWithMultipleImageModelMultipleImagePostID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_MultipleImageData_tbl_MultipleImagePost_APostWithMultipleImageModelMultipleImagePostID",
                table: "tbl_MultipleImageData",
                column: "APostWithMultipleImageModelMultipleImagePostID",
                principalTable: "tbl_MultipleImagePost",
                principalColumn: "MultipleImagePostID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
