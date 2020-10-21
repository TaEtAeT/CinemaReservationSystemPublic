using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.Utility
{
    public static class SD
    {
        public const string Role_User = "Plain User";


        //admin can also manage the content of the website, like
        //mvies, prices
        public const string Role_Admin = "Admin";
     
        //emplyees can take payments, etc
        public const string Role_Employee = "Employee";
    }
}
