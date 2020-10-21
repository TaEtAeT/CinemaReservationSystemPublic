using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaReservationSystem.DataAccess.Migrations
{
    public partial class ChangedForeignKeyNameRservstionInReservedSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Reservations_ResevationId",
                table: "ReservedSeats");

            migrationBuilder.DropIndex(
                name: "IX_ReservedSeats_ResevationId",
                table: "ReservedSeats");

            migrationBuilder.DropColumn(
                name: "ResevationId",
                table: "ReservedSeats");
      
            migrationBuilder.CreateIndex(
                name: "IX_ReservedSeats_ReservationId",
                table: "ReservedSeats",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Reservations_ReservationId",
                table: "ReservedSeats",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedSeats_Reservations_ReservationId",
                table: "ReservedSeats");

            migrationBuilder.DropIndex(
                name: "IX_ReservedSeats_ReservationId",
                table: "ReservedSeats");


            migrationBuilder.AddColumn<int>(
                name: "ResevationId",
                table: "ReservedSeats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservedSeats_ResevationId",
                table: "ReservedSeats",
                column: "ResevationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedSeats_Reservations_ResevationId",
                table: "ReservedSeats",
                column: "ResevationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
