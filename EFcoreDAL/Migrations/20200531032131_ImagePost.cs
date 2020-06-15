using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class ImagePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "tbl_MultipleImagePost",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "tbl_MultipleImagePost",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tbl_MultipleImagePost",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "tbl_MultipleImagePost",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "tbl_MultipleImagePost",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "tbl_MultipleImagePost",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "tbl_MultipleImagePost");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "tbl_MultipleImagePost");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tbl_MultipleImagePost");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "tbl_MultipleImagePost");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "tbl_MultipleImagePost");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "tbl_MultipleImagePost");
        }
    }
}
