using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public  interface IAuditoriumRepository:IRepository<Auditorium>
    {
        public void Update(Auditorium auditorium);
    }
}
