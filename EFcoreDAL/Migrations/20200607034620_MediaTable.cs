using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFcoreDAL.Migrations
{
    public partial class MediaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "tbl_Post",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateTable(
                name: "tbl_MediaPost",
                columns: table => new
                {
                    MediaPostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaPostPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaPostDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MediaViews = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MediaPost", x => x.MediaPostID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MediaPost");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "tbl_Post",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
