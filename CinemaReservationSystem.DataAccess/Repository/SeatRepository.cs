using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public readonly ApplicationDbContext _db;
        public SeatRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Seat> GetSeatsForAuditorium(Auditorium auditorium)
        {
            var seats = _db.Seats.Where(s => s.Auditorium == auditorium);
            return seats;
        }

        public void Update(Seat seat)
        {
            var objFromDb = _db.Seats.FirstOrDefault(s => s.Id == seat.Id);
            objFromDb.RowLetter = seat.RowLetter;
            objFromDb.Number = seat.Number;
            objFromDb.AuditoriumId = seat.AuditoriumId;
          
        }
    }
}
