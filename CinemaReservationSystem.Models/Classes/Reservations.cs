using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.Models.Classes
{
    public class Reservations
    {
        public Reservation Reservation { get; set; }
        public string Seats { get; set; }

        public Reservations(Reservation reservation)
        {
            Reservation = reservation;
            Seats = string.Join(",", reservation.ReservedSeats.Select(s => s.Seat.Code));
        }


    }
}