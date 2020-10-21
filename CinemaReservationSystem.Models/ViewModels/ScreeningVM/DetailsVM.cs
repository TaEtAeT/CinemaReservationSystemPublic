using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.Models.ViewModels.ScreeningVM
{
   public class DetailsVM
    {
        public IEnumerable<Screening> Screenings;
        public Movie Movie;

        public DetailsVM(IEnumerable<Screening> screenings, Movie movie)
        {
            Screenings = screenings;
            Movie = movie;
        }
    }
}
