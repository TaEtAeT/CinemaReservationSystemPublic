using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface INewsRepository: IRepository<News>
    {
        public void Update(News news);
        public IEnumerable<News> GetNewsPerPage(int excludeRecords, int pageSize);
    }
}
