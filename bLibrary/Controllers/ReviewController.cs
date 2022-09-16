using bLibrary.API;
using bLibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bLibrary.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ClientManager manager;
        public ReviewController()
        {
            manager = new ClientManager();
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public IActionResult GetCreateReview() => View();
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public IActionResult GetEditReview(Guid? id)
        {
            var review = manager.GetBookById(id);
            if (review != null)
                return View(review);
            else
                return NotFound();
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public IActionResult CreateReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.CreateDate = DateTime.Now;
                manager.CreateReview(review);
                return RedirectToAction("GetBook", "Book", new { id = review.Book.Id });
            }
            else
                return StatusCode(422);
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public IActionResult EditReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.CreateDate = DateTime.Now;
                manager.EditReview(review);
                return RedirectToAction("GetBook", "Book", new { id = review.Book.Id });
            }
            else
                return StatusCode(422);
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public IActionResult DeleteReview(Guid? id)
        {
            var review = manager.DeleteReview(id);
            return RedirectToAction("GetBook", "Book", new { id = review.Book.Id });
        }
    }
}
