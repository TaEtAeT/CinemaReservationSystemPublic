using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IScreeningRepository:IRepository<Screening>
    {
        public void Update(Screening screening);
        public IEnumerable<Screening> GetScreenings();
        /*  public Task<IEnumerable<Screening>> GetScreeningsForMovie(int movieId);*/
        public IEnumerable<Screening> GetScreeningsForMovie(int movieId);
        public Screening GetScreeningById(int id);
        /*      public IEnumerable<Screening> GetScreeningsForInterval(string date, string time, Auditorium auditorium);*/

        public IEnumerable<Screening> GetScreeningsWithGenre(Genre genre);
    }
}
