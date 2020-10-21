using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CinemaReservationSystem.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Director { get; set; }

        public string Description { get; set; }

        [Display(Name = "Running Time")]
        public int RunningTimeMin { get; set; }


        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }


        public string ImageUrl { get; set; }
        public string TrailerUrl { get; set; }

        public string RuntimeText{
            get
                {


               int Hour = RunningTimeMin / 60;
                int Minute = RunningTimeMin % 60;
                string runTimeText = FormatTwoDigits(Hour) + "h" +" "+ FormatTwoDigits(Minute) + "m";
                return runTimeText;
            } 
         }
        private string FormatTwoDigits(int i)
        {
            string functionReturnValue = "";
            if (10 > i)
            {
                functionReturnValue = "0" + i.ToString();
            }
            else
            {
                functionReturnValue = i.ToString();
            }
            return functionReturnValue;
        }
    }
}
