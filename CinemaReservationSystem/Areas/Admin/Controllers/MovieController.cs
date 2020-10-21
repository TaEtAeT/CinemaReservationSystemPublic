using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels.MovieVM;
using CinemaReservationSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaReservationSystem.Areas.Amin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MovieController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<Genre> GenList =  _unitOfWork.Genre.GetAll();
            UpsertVM movieVM = new UpsertVM() {

                Movie = new Movie(),
                GenreList = GenList.Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                })
            };
            if(id == null)
            {
                return View(movieVM);
            }
            //for edit
            movieVM.Movie = _unitOfWork.Movie.Get(id.GetValueOrDefault());
            if (movieVM == null)
            {
                return NotFound();
            }
            return View(movieVM);
    

        }
        #region API_CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Movie.GetMovies();
            return Json(new { data = allObj });
        }


        [HttpPost]
          [ValidateAntiForgeryToken]
        public IActionResult Upsert(UpsertVM movieVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\movies");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (movieVM.Movie.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, movieVM.Movie.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    movieVM.Movie.ImageUrl = @"\images\movies\" + fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (movieVM.Movie.Id != 0)
                    {
                        Movie objFromDb = _unitOfWork.Movie.Get(movieVM.Movie.Id);
                        movieVM.Movie.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                if (movieVM.Movie.Id == 0)
                {
                    _unitOfWork.Movie.Add(movieVM.Movie);

                }
                else
                {
                    _unitOfWork.Movie.Update(movieVM.Movie);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (movieVM.Movie.Id != 0)
                {
                    movieVM.Movie = _unitOfWork.Movie.Get(movieVM.Movie.Id);
                }
            }
            return View(movieVM);
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Movie.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Movie.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
