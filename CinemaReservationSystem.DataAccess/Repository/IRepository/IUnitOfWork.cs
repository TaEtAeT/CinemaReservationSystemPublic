using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movie { get; }
        IGenreRepository Genre { get; }
        IAuditoriumRepository Auditorium { get; }
        IScreeningRepository Screening { get; }
        ISeatRepository Seat { get; }

        IReservationRepository Reservation { get;  }
        IReservedSeatRepository ReservedSeat { get;  }

        INewsRepository News { get;  }
        IAplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
