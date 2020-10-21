using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.AspNetCore.Builder;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Movie = new MovieRepository(_db);
            Genre = new GenreRepository(_db);
            Auditorium = new AuditoriumRepository(_db);
            Screening = new ScreeningRepository(_db);
            Seat = new SeatRepository(_db);
            Reservation = new ReservationRepository(_db);
            ReservedSeat = new ReservedSeatRepository(_db);
            News = new NewsRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);

        }
        public IMovieRepository Movie { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IAuditoriumRepository Auditorium { get; private set; }
        public IScreeningRepository Screening { get; private set; }
        public ISeatRepository Seat { get; private set; }
        public IReservationRepository Reservation { get; private set; }
        public IReservedSeatRepository ReservedSeat { get; private set; }
        public INewsRepository News { get; private set; }
        public IAplicationUserRepository ApplicationUser { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
