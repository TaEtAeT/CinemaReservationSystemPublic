using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaReservationSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
