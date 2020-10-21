using System;
using System.IO;
using CinemaReservationSystem.DataAccess.Repository.IRepository;
using CinemaReservationSystem.Models;
using CinemaReservationSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaReservationSystem.Areas.Amin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            News news = new News();
            if(id == null)
            {
                return View(news);
            }
            //for edit
            news = _unitOfWork.News.Get(id.GetValueOrDefault());
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
    

        }
        #region API_CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.News.GetAll();
            return Json(new { data = allObj });
        }
        [HttpPost]
          [ValidateAntiForgeryToken]
        public IActionResult Upsert(News news)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\news");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (news.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, news.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    news.ImageUrl = @"\images\news\" + fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (news.Id != 0)
                    {
                        News objFromDb = _unitOfWork.News.Get(news.Id);
                        news.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                if (news.Id == 0)
                {
                    _unitOfWork.News.Add(news);

                }
                else
                {
                    _unitOfWork.News.Update(news);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (news.Id != 0)
                {
                    news = _unitOfWork.News.Get(news.Id);
                }
            }
            return View(news);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.News.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.News.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
