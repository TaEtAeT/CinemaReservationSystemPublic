using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public readonly ApplicationDbContext _db;

        public ReservationRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public IEnumerable<Reservation> GetReservationsForUser(string userId)
        {
            return _db.Reservations.Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Screening)
                .Include(r => r.Screening.Movie)
                .Include(r => r.ReservedSeats)
                .ThenInclude(rs=> rs.Seat)
              .Include(r => r.Screening.Auditorium).ToList();
        }

        public void Update(Reservation reservation)
        {
            var reservationObj = _db.Reservations.FirstOrDefault(r => r.Id == reservation.Id);

        }
    }
}
