using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.Models.ViewModels.ScreeningVM
{
    public class IndexVM
    {
        public IEnumerable<Screening> Screenings { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public IndexVM(IEnumerable<Screening> screenings, IEnumerable<Genre> genres )
        {
            Screenings = screenings;
            Genres = genres;
        }
    }
}
