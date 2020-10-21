using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CinemaReservationSystem.Models
{
    //A reservation has multiple seats.
    public class ReservedSeat
    {

        [Key]
        public int Id { get; set; }

        public int SeatId { get; set; }
        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }


        public int ScreeningId { get; set; }
        [ForeignKey("ScreeningId")]
        public Screening Screening { get; set; }


        public int ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }


        public bool isReserved { get; set; }

        public ReservedSeat(Seat seat, Screening screening, Reservation reservation, bool isreserved)
        {
            Seat = seat;
            Screening = screening;
            Reservation = reservation;
            isReserved = isreserved;
        }

        public ReservedSeat()
        {

        }

    }
}
