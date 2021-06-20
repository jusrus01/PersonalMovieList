using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalMovieListApi.Migrations
{
    public partial class AddedImageToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Movies",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");
        }
    }
}
