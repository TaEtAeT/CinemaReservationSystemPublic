using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaReservationSystem.DataAccess.Migrations
{
    public partial class AddTrailerUrlToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "Movies");

        }
    }
}
