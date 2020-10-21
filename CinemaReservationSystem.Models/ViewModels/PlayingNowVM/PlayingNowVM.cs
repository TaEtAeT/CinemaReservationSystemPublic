using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaReservationSystem.Models.ViewModels.PlayingNowVM
{
    public class PlayingNowVM
    {

        public IEnumerable<Screening> Screenings { get; set; }

        public List<SelectListItem> GenresList { get; set; }
        public int GenreId { get; set; }

        public PlayingNowVM(IEnumerable<Screening> screenings, List<SelectListItem> genresList )
        {
            Screenings = screenings;
            GenresList = genresList;
        }

    }
}
