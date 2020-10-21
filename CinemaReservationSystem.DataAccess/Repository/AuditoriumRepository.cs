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
    public class AuditoriumRepository : Repository<Auditorium>, IAuditoriumRepository
    {
        public readonly ApplicationDbContext _db;
        public AuditoriumRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Auditorium auditorium)
        {
            var objFromDb = _db.Auditoriums.FirstOrDefault(a => a.Id == auditorium.Id);
            objFromDb.Name = auditorium.Name;
            objFromDb.SeatsNo = auditorium.SeatsNo;
        }
    }
}
