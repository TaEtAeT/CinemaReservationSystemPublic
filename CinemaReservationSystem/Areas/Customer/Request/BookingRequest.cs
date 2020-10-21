using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Areas.Customer.Request
{
    public class BookingRequest
    {
        public int screeningId { get; set; }
        public string seatsList { get; set; }
    }
}
