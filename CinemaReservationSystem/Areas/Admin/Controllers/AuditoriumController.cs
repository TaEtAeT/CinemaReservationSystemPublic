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
    public class AuditoriumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditoriumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Auditorium auditorium = new Auditorium();
            if(id == null)
            {
                return View(auditorium);
            }
            //for edit
            var objFromDb = _unitOfWork.Auditorium.Get(id.GetValueOrDefault());
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
            var allObj = _unitOfWork.Auditorium.GetAll();
            return Json(new { data = allObj });
        }
        [HttpPost]
          [ValidateAntiForgeryToken]
        public IActionResult Upsert(Auditorium auditorium)
        {
            if (ModelState.IsValid) {

                if (auditorium.Id == 0)
                {
                    //insert
                     _unitOfWork.Auditorium.Add(auditorium);

                }
                else
                {
                    //update
                    _unitOfWork.Auditorium.Update(auditorium);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); 
            }

            return View(auditorium);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Auditorium.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Auditorium.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
