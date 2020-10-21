using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Admin,Employee,Plain User")]
    public class UserBookingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = _unitOfWork.Reservation.GetReservationsForUser(userId).Select(i => new Reservations(i));
            return Json(new { data = reservations });
        }

        [HttpGet]
        public IActionResult Buy(int id) {

            var reservationId = id;
            var reservation = _unitOfWork.Reservation.Get(reservationId);
            if (!reservation.isCanceled)
            {
                reservation.isPaid = true;
                _unitOfWork.Reservation.Update(reservation);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Tickets paid successfully!" });
            }

            return Json(new { success = false, message = "You can't pay for a cancelled reservation!" });
        }

        [HttpGet]
        public IActionResult Cancel(int id)
        {
            var reservationId = id;
            var reservation = _unitOfWork.Reservation.Get(reservationId);
            reservation.isCanceled = true;
            _unitOfWork.Reservation.Update(reservation);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Reservation cancelled successfully!" });
        }
    }
}
