using BulkyBook.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Repository.IRepository
{
    public interface IAplicationUserRepository:IRepository<ApplicationUser>
    {
        //retreieve users with roles

        public IEnumerable<ApplicationUser> GetUsersWithRoles();
        public ApplicationUser GetApplicationUser(string userId);
    }
}
