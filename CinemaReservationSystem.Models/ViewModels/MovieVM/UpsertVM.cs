using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CinemaReservationSystem.Models.ViewModels.MovieVM
{
    public class UpsertVM
    {
        public Movie Movie { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
