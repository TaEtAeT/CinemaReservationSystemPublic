using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IReservedSeatRepository:IRepository<ReservedSeat>
    {
        public void Update(ReservedSeat reservedSeat);
        public IEnumerable<ReservedSeat> GetWhere(ReservedSeat seat, Screening screening);
        public IEnumerable<ReservedSeat> GetAllForScreening(Screening screening);
    }
}
