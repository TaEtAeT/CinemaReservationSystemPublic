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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IAplicationUserRepository
    {
        public readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public ApplicationUser GetApplicationUser(string userId)
        {
            return _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<ApplicationUser> GetUsersWithRoles()
        {
            //have no idea how this happened
            var userList = _db.ApplicationUsers;
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach(var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

            }
            return userList;
          /*  return _db.ApplicationUsers.Include(u => u.Role).ToList();*/
        }
    }
}
