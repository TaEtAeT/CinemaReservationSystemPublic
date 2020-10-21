using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaReservationSystem.DataAccess.Migrations
{
    public partial class ChangedRowLetterToStringInSeatTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Screenings_ScreeningId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Screenings_ScreeningId",
                table: "ReservedSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Seats_SeatId",
                table: "ReservedSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Auditoriums_AuditoriumId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Auditoriums_AuditoriumId",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "RowLetter",
                table: "Seats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Screenings_ScreeningId",
                table: "Reservations",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Screenings_ScreeningId",
                table: "ReservedSeats",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Seats_SeatId",
                table: "ReservedSeats",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Auditoriums_AuditoriumId",
                table: "Screenings",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Auditoriums_AuditoriumId",
                table: "Seats",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Screenings_ScreeningId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Screenings_ScreeningId",
                table: "ReservedSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Seats_SeatId",
                table: "ReservedSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Auditoriums_AuditoriumId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Auditoriums_AuditoriumId",
                table: "Seats");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92e3a0ae-8cee-48ad-95fc-d91a3537a684");

            migrationBuilder.AlterColumn<int>(
                name: "RowLetter",
                table: "Seats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Screenings_ScreeningId",
                table: "Reservations",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Screenings_ScreeningId",
                table: "ReservedSeats",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Seats_SeatId",
                table: "ReservedSeats",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Auditoriums_AuditoriumId",
                table: "Screenings",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Auditoriums_AuditoriumId",
                table: "Seats",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
