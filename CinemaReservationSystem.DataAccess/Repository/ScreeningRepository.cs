using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class ScreeningRepository : Repository<Screening>, IScreeningRepository
    {
        public readonly ApplicationDbContext _db;
        public ScreeningRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Screening> GetScreenings()
        {
            return  _db.Screenings.Include(s => s.Auditorium).Include(s => s.Movie).Include(g => g.Movie.Genre).ToList();
        }

        public Screening GetScreeningById(int id)
        {
            return _db.Screenings.Include(s => s.Auditorium).Include(s => s.Movie).Include(g => g.Movie.Genre).Where(s => s.Id == id).FirstOrDefault();
        }

        //get all screenings for a movie

        /*  public async Task<IEnumerable<Screening>> GetScreeningsForMovie(int movieId)
          {

              return await _db.Screenings.Where(screening => screening.MovieId == movieId)

                  .Include(s => s.Auditorium).Include(s => s.Movie).Include(g => g.Movie.Genre).ToListAsync();
          }


  */
        public IEnumerable<Screening> GetScreeningsForMovie(int movieId)
        {

            return  _db.Screenings.Where(screening => screening.MovieId == movieId)

                .Include(s => s.Auditorium).Include(s => s.Movie).Include(g => g.Movie.Genre).ToList();
        }


        public void Update(Screening screening)
        {
            var objFromDb = _db.Screenings.FirstOrDefault(s => s.Id == screening.Id);
            objFromDb.MovieId = screening.MovieId;
            objFromDb.AuditoriumId = screening.AuditoriumId;
            objFromDb.ScreeningStart = screening.ScreeningStart;
            objFromDb.TicketPrice = screening.TicketPrice;
            objFromDb.Date = screening.Date;
            objFromDb.isPreview = screening.isPreview;
        }

   /*     public IEnumerable<Screening> GetScreeningsForInterval(string date, string time, Auditorium auditorium)
        {
            //get all screenings in that day
            var screeningsForDay = _db.Screenings.Where(s => s.Date == date);

            var screeningsForAuditorium = screeningsForDay.Where(s => s.Auditorium == auditorium);

            //screening start to min

      
            var screeningsForTime = screeningsForAuditorium.Where(s => s.ScreeningStart) >= StringTimeToMin(time)
            &&
             StringTimeToMin(s.ScreeningStart) <= (StringTimeToMin(time) + 4)
            ).ToList();
            return screeningsForTime;
        }*/

        public int StringTimeToMin(string time)
        {
            int charLocation = time.IndexOf(":", StringComparison.Ordinal);
            string hours;
            string minutes;

                hours = time.Substring(0, charLocation);
                minutes = time.Substring(charLocation, 3);
            if (hours.StartsWith("0"))
            {
                hours = hours.Substring(1, 1);
            }

            return int.Parse(hours) * 60 + int.Parse(minutes);
           
        }

        public IEnumerable<Screening> GetScreeningsWithGenre(Genre genre)
        {
            return _db.Screenings.Where(s => s.Movie.Genre == genre)
                .Include(s => s.Movie).Include(s => s.Auditorium).Include
                (s => s.Movie.Genre)
                .ToList();
           /* .Include(s => s.Movie).Include(s => s.Auditorium).Include
                (s => s.Movie.Genre)*/
        }
    }
}
