using BulkyBook.DataAccess.Repository;
using CinemaReservationSystem.DataAccess.Data;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public readonly ApplicationDbContext _db;
        public NewsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<News> GetNewsPerPage(int excludeRecords, int pageSize)
        {
            return _db.News.AsNoTracking().Skip(excludeRecords).Take(pageSize).ToList();
        }

        public void Update(News news)
        {
            var objFromDb = _db.News.FirstOrDefault(n => n.Id == news.Id);
            if (news.ImageUrl != null)
            {
                objFromDb.ImageUrl = news.ImageUrl;
            }
            objFromDb.Title = news.Title;
            objFromDb.ShortDescription = news.ShortDescription;
            objFromDb.Content = news.Content;
            objFromDb.Date = news.Date;

        }
    }
}
