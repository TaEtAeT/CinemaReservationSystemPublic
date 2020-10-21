using CinemaReservationSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CinemaReservationSystem.Models
{
    public class Screening
    {
        //keeps account of currently playing movies and their playing room

        [Key]
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
        [ForeignKey("AuditoriumId")]
        public Auditorium Auditorium { get; set; }

        [Required]
        public string ScreeningStart { get; set; }

        [Required]

        public double  TicketPrice { get; set; }

        public string Date { get; set; }

        public bool isPreview { get; set; }

    }


}
