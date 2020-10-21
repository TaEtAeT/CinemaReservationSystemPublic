using CinemaReservationSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaReservationSystem.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RowLetter { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
        [ForeignKey("AuditoriumId")]
        public Auditorium Auditorium { get; set; }

        [NotMapped]
        public string Code
        {
            get
            {
                return $"{RowLetter}{Number}";
            }
        }


    }
}
