using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class ReservedSeatRepository : Repository<ReservedSeat>, IReservedSeatRepository
    {
        public readonly ApplicationDbContext _db;

        public ReservedSeatRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public IEnumerable<ReservedSeat> GetAllForScreening(Screening screening)
        {
            return _db.ReservedSeats.Where(rs => rs.Screening == screening).ToList();
        }

        public IEnumerable<ReservedSeat> GetWhere(ReservedSeat rSeat, Screening screening)
        {
            return _db.ReservedSeats.Where(rs => rs.Seat.Id ==  rSeat.Seat.Id 
            
            &&
            
            rs.Screening.Id == screening.Id
            ).ToList();

        }

        public void Update(ReservedSeat reservedSeat)
        {
            //seat id, screening id, reservation id


            var objFromDb = _db.ReservedSeats.FirstOrDefault(rs => rs.Id == reservedSeat.Id);
            objFromDb.Seat = reservedSeat.Seat;
            objFromDb.Screening = reservedSeat.Screening;
            objFromDb.Reservation = reservedSeat.Reservation;
        }
    }
}
