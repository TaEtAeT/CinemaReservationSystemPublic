using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IReservationRepository:IRepository<Reservation>
    {
        public void Update(Reservation reservation);
        public IEnumerable<Reservation> GetReservationsForUser(string userId);
    }
}
