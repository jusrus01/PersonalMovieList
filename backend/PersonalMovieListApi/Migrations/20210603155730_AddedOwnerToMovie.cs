using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalMovieListApi.Migrations
{
    public partial class AddedOwnerToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "Movies");
        }
    }
}
