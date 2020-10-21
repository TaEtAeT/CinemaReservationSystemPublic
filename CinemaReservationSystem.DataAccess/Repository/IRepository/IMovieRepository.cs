using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        void Update(Movie movie);

        public IEnumerable<Movie> GetMovies();

        public Movie GetMovieWithDetails(int movieId);
    }
}
