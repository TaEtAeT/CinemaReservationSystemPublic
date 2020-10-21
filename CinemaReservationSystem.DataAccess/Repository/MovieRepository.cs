using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public readonly ApplicationDbContext _db;
        public MovieRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _db.Movies.Include(m => m.Genre).ToList();
        }

        public Movie GetMovieWithDetails(int movieId)
        {
            return _db.Movies.Where(m => m.Id == movieId).Include(m => m.Genre).ToList().FirstOrDefault();

            /* Include(m => m.Genre).ToList();*/
        }

        public void Update(Movie movie)
        {
            var objFromDb = _db.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (movie.ImageUrl != null)
            {
                objFromDb.ImageUrl = movie.ImageUrl;
            }
            objFromDb.Title = movie.Title;
            objFromDb.Description = movie.Description;
            objFromDb.Genre = movie.Genre;
            objFromDb.RunningTimeMin = movie.RunningTimeMin;
            objFromDb.TrailerUrl = movie.TrailerUrl;

        }

       
    }
}
