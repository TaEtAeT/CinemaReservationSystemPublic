using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaReservationSystem.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int ScreeningId { get; set; }
        [ForeignKey("ScreeningId")]
        public Screening Screening { get; set; }

        public IEnumerable<ReservedSeat> ReservedSeats { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public double AmountToPay { get; set; }

        public bool isPaid { get; set; }

        public bool isCanceled { get; set; }
   

    }
}
