using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaReservationSystem.Areas.Customer.Request;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Models.ViewModels.BookingVM;
using CinemaReservationSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaReservationSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Admin,Employee,Plain User")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
  

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int id)
        {
            //var screening = _unitOfWork.Screening.GetScreenings().Where(s => s.Id == id).FirstOrDefault();
            var screening = _unitOfWork.Screening.GetScreeningById(id);
            var seatsForScreening = _unitOfWork.Seat.GetSeatsForAuditorium(screening.Auditorium);
            var rSeatsList = _unitOfWork.ReservedSeat.GetAllForScreening(screening);
            BookingVM bookingVM = new BookingVM(screening, seatsForScreening, rSeatsList);
            return View(bookingVM);
        }

        #region API_CALLS

        [HttpGet] 
        public IActionResult Book(BookingRequest data)
        {
            //deconstruct data
            int screeningId = data.screeningId;
            if (data.seatsList == null) {
                return Json(new { success = false, message = "Please select at least one seat!" });

            }
            string seatsStringList = data.seatsList;

            List<string> seatsList = seatsStringList.Split(',').ToList<string>();
            List<int> seatsIdList = seatsList.Select(int.Parse).ToList();

            Screening screening = _unitOfWork.Screening.Get(screeningId);
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //create reservation (screening, user, amountToPay, isPaid, isCancelled)
           var reservation =  new Reservation
            {
                Screening = screening,
                User = _unitOfWork.ApplicationUser.GetApplicationUser(userId),
                AmountToPay = 0,
                isPaid = false,
                isCanceled = false
            };

            //create reserved seats for the seats selected(seat, screening, reservation, isReserved)
            //foreach seat
            //create new list of reserved seats
            List<ReservedSeat> reservedSeatsList = new List<ReservedSeat>();
            foreach (var seatId in seatsIdList)
            {
                //find the seat
                var seat = _unitOfWork.Seat.Get(seatId);
                reservedSeatsList.Add(
                    new ReservedSeat(seat, screening, reservation, true));
            }

/*
            //attach the reserved seats to the reservation -- no need*/

            //and calculate amount to pay
            int seatNo = seatsIdList.Count;
            double screeningPrice = screening.TicketPrice;
            var amountToPay = seatNo * screeningPrice;
            //save amount to Pay in reservation
            reservation.AmountToPay = amountToPay;
            //save reservation

            //save reserved seats
            _unitOfWork.Reservation.Add(reservation);
            int currentRSeats = 0;
            foreach(var reservedSeat in reservedSeatsList)
            {
                //if reserved seat doesn t already exist---------
                var rSeatFromDb = _unitOfWork.ReservedSeat.GetWhere(reservedSeat, screening);


                if (rSeatFromDb.Count() == 0)
                {
                    _unitOfWork.ReservedSeat.Add(reservedSeat);
                    currentRSeats ++;
             
                }
    
            }
            //only if there's a reserved seat save the reservation and reserved seat

            int seatsToBook = seatsIdList.Count();
           if (currentRSeats == seatsToBook)
            {
                _unitOfWork.Save();
                return Json(new { success = true, message = "Booked Successfully! See your bookings!" });
            }
            return Json(new { success= false, message= "At least 1 seat already taken! Please select only empty seats!"});

        }

        #endregion
    }
}
