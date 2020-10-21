using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public readonly ApplicationDbContext _db;
        public GenreRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public List<SelectListItem> GetGenreList(IEnumerable<Genre> genreList)
        {
            return _db.Genres
                .Select(
                a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();
        }

        public void Update(Genre genre)
        {
            var objFromDb = _db.Genres.FirstOrDefault(g => g.Id == genre.Id);
            objFromDb.Name = genre.Name;
            objFromDb.Description = genre.Description;

        }
    }
}
