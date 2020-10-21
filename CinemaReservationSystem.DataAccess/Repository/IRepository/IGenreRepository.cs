using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IGenreRepository: IRepository<Genre>
    {
        public void Update(Genre genre);
        public List<SelectListItem> GetGenreList(IEnumerable<Genre> genreList);
    }
}
