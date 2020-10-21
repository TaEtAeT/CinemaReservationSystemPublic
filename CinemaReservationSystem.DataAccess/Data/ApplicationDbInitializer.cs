using CinemaReservationSystem.Models;
using CinemaReservationSystem.Utility;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.DataAccess.Data
{
   public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = roleManager.FindByNameAsync(SD.Role_Admin).Result;
            if (adminRole == null)
            {
                var role = new IdentityRole
                {
                    Name = SD.Role_Admin,
                };
                roleManager.CreateAsync(role).Wait();
            }
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Admin123*").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, SD.Role_Admin).Wait();
                }
            }
        }
    }
}
