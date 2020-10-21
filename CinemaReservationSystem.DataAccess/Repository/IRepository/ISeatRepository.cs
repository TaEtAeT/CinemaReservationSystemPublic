using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface ISeatRepository:IRepository<Seat>
    {
        public void Update(Seat seat);
        public IEnumerable<Seat> GetSeatsForAuditorium(Auditorium auditorium);
    }
}
