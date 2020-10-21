using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.Models.ViewModels.BookingVM
{
    public class BookingVM
    {
        public Screening Screening { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public IEnumerable<ReservedSeat> ReservedSeats { get; set; }

        public IEnumerable<IEnumerable<Seat>> ListOfOrderedSeats
        {
            get
            {
                var orderedSeats = Seats.OrderBy(seat => seat.RowLetter);
                int size = 10;

                for (var i = 0; i < orderedSeats.Count() / size; i++)
                {
                    yield return orderedSeats.Skip(i * size).Take(size);
                }

            }
        }

        public IEnumerable<int> ListOfIdsForReservedSeats
        {
            get
            {
                return ReservedSeats.Select(rs => rs.SeatId);
            }
        }
        public IEnumerable<String> ListOfRows
        {
            get
            {
                return Seats.Select(s => s.RowLetter).Distinct().OrderBy(e => e);
            }
        }

        public BookingVM(Screening screening, IEnumerable<Seat> seats, IEnumerable<ReservedSeat> rSeats)
        {
            Screening = screening;
            Seats = seats;
            ReservedSeats = rSeats;
        }

    }
}
