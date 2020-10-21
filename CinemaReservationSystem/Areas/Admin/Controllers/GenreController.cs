using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.Areas.Amin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Genre genre = new Genre();
            if(id == null)
            {
                return View(genre);
            }
            //for edit
            var objFromDb = _unitOfWork.Genre.Get(id.GetValueOrDefault());
            if (objFromDb == null)
            {
                return NotFound();
            }
            return View(objFromDb);
    

        }
        #region API_CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Genre.GetAll();
            return Json(new { data = allObj });
        }
        [HttpPost]
          [ValidateAntiForgeryToken]
        public IActionResult Upsert(Genre genre)
        {
            if (ModelState.IsValid) {

                if (genre.Id == 0)
                {
                    //insert
                     _unitOfWork.Genre.Add(genre);

                }
                else
                {
                    //update
                    _unitOfWork.Genre.Update(genre);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); 
            }

            return View(genre);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Genre.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Genre.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
