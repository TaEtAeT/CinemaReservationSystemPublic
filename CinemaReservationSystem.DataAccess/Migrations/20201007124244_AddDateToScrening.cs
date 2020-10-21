using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaReservationSystem.DataAccess.Migrations
{
    public partial class AddDateToScrening : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Screenings");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Screenings",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Screenings");

            migrationBuilder.AddColumn<string>(
                name: "WeekDay",
                table: "Screenings",
                type: "nvarchar(max)",
                nullable: true);

        }
    }
}
