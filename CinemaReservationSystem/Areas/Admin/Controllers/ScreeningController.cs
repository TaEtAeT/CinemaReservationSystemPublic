using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels.ScreeningVM;
using CinemaReservationSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaReservationSystem.Areas.Amin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ScreeningController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ScreeningController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Details(int movieId)
        {
            //return a list of screenings for a movie Id
            var screeningsForMovie =   _unitOfWork.Screening.GetScreeningsForMovie(movieId);
            var movie = _unitOfWork.Movie.GetMovieWithDetails(movieId);

            var VM = new DetailsVM(screeningsForMovie, movie);
           
            return View(VM);

        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<Movie> movieList = _unitOfWork.Movie.GetAll();
            IEnumerable<Auditorium> auditoriumList = _unitOfWork.Auditorium.GetAll();

            UpsertVM screeningVM = new UpsertVM()
            {
                Screening = new Screening(),
                MovieList = movieList.Select(m => new SelectListItem
                {
                    Text = m.Title,
                    Value = m.Id.ToString()
                }),
                AuditoriumList = auditoriumList.Select(a => new SelectListItem{
                    Text = a.Name,
                    Value = a.Id.ToString()
                })
               
            };
          
            if (id == null)
            {
                return View(screeningVM);
            }
            //for edit
            screeningVM.Screening = _unitOfWork.Screening.Get(id.GetValueOrDefault());
            if (screeningVM == null)
            {
                return NotFound();
            }
            return View(screeningVM);


        }
        #region API_CALLS
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Screening.GetScreenings();
            return Json(new { data = allObj });
        }


        [HttpGet]
        [AllowAnonymous]

        //////!!!!!!!!!!!!!!!! new 
        /////RETURN A SCREENING NOT A DETAILSVM!!!!!!!!
        public IActionResult GetScreeningsForMovie([FromQuery] int movieId)
        {
            //return a list of screenings for a movie Id
            var allObj = _unitOfWork.Screening.GetScreeningsForMovie(movieId);
            return Json(new { data = allObj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UpsertVM screeningVM)
        {
            if (ModelState.IsValid)
            {
                if(screeningVM.Screening.Id == 0)
                {
                    //insert

                    //check if time is available
                    //that is current time or 4h after current time
                    //so take the date and the auditorium of the screening you try to create
                /*    var date = screeningVM.Screening.Date;
                    var time = screeningVM.Screening.ScreeningStart;
                    var auditorium = screeningVM.Screening.Auditorium;*/

                    //check if there's a screening in the db in that day and in that time interval
                  //  var screeningListFromDb = _unitOfWork.Screening.GetScreeningsForInterval(date, time, auditorium);

                    //if it's preview, add 20% to price
                    if(screeningVM.Screening.isPreview == true)
                    {
                        screeningVM.Screening.TicketPrice = (20 * screeningVM.Screening.TicketPrice) + screeningVM.Screening.TicketPrice;
                    }
                    _unitOfWork.Screening.Add(screeningVM.Screening);
                }
                else
                {
                    //edit

                    //check if time is available
                    //that is current time or 4h after current time
                    //so take the date and the auditorium of the screening you try to edit

                    /*  var date = screeningVM.Screening.Date;
                      var time = screeningVM.Screening.ScreeningStart;
                      var auditorium = screeningVM.Screening.Auditorium;*/

                    //check if there's a screening in the db in that day and in that time interval
                    // var screeningListFromDb = _unitOfWork.Screening.GetScreeningsForInterval(date, time, auditorium);
                    if (screeningVM.Screening.isPreview == true)
                    {
                        screeningVM.Screening.TicketPrice = (20/100) * screeningVM.Screening.TicketPrice + screeningVM.Screening.TicketPrice;
                    }
                    _unitOfWork.Screening.Update(screeningVM.Screening);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(screeningVM);
        }

            [HttpDelete]

        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Screening.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Screening.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
