using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels;
using CinemaReservationSystem.Models.ViewModels.PlayingNowVM;
using CinemaReservationSystem.Models.ViewModels.ScreeningVM;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaReservationSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Movie> movies = _unitOfWork.Movie.GetMovies();
            IEnumerable<Screening> playingNowMovies = _unitOfWork.Screening.GetAll().GroupBy(m => m.MovieId).Select(s => s.First());
            Console.WriteLine(playingNowMovies.Count());
            PlayingNowVM playingNowVM = new PlayingNowVM(playingNowMovies, _unitOfWork.Genre.GetGenreList(_unitOfWork.Genre.GetAll()));
            return View("Index",playingNowVM);
        }

        public IActionResult News(int pageNumber = 1, int pageSize = 2)
        {
            int excludeRecords = (pageNumber * pageSize) - pageSize;
            var news = _unitOfWork.News.GetNewsPerPage(excludeRecords, pageSize);
            var result = new PagedResult<News>
            {
                Data = news.ToList(),
                TotalItems = _unitOfWork.News.GetAll().Count(),
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API_CALLS

        [HttpGet]
        public PartialViewResult Sort([FromQuery] string genreId)
        {
            if (String.IsNullOrEmpty(genreId) || genreId =="-1")
            {
                IEnumerable<Movie> movies = _unitOfWork.Movie.GetMovies();
                IEnumerable<Screening> plNowMovies = _unitOfWork.Screening.GetAll().GroupBy(m => m.MovieId).Select(s => s.First());
                PlayingNowVM plNowVM = new PlayingNowVM(plNowMovies, _unitOfWork.Genre.GetGenreList(_unitOfWork.Genre.GetAll()));

                return PartialView("_PlayingNowMovies", plNowVM);
            }
            var genre = _unitOfWork.Genre.Get(int.Parse(genreId));
        
            IEnumerable<Screening> playingNowMovies = _unitOfWork.Screening.GetScreeningsWithGenre(genre).GroupBy(m => m.MovieId).Select(s => s.First());
            PlayingNowVM playingNowVM = new PlayingNowVM(playingNowMovies, _unitOfWork.Genre.GetGenreList(_unitOfWork.Genre.GetAll()));

            return PartialView("_PlayingNowMovies", playingNowVM);
        }
        #endregion

    }
}
