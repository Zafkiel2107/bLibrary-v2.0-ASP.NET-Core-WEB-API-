using bLibrary.API;
using bLibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace bLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly ClientManager manager;
        const int pageSize = 20;
        public BookController()
        {
            manager = new ClientManager();
        }
        [HttpGet, ValidateAntiForgeryToken]
        public IActionResult GetBooks(int? page, string action = null)
        {
            ViewBag.action = action;
            int pageNumber = page ?? 1;
            IPagedList<Book> books = manager.GetBooks()?.OrderByDescending(x => x.RecommendationsNum).ToPagedList(pageNumber, pageSize);
            return View(books);
        }
        [HttpGet, ValidateAntiForgeryToken]
        public IActionResult GetBook(Guid? id)
        {
            var book = manager.GetBookById(id);
            ViewBag.Review = manager.GetReviews().FirstOrDefault(x => x.User == User.Identity && x.Book.Id == book.Id);
            return View(book);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public IActionResult GetCreateBook()
        {
            ViewBag.Genres = manager.GetGenres().Select(x => x.Name);
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public IActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                if (!book.BookLink.Contains("pdf") && !book.CoverLink.Contains("jpg"))
                {
                    return StatusCode(422);
                }
                manager.CreateBook(book);
                return RedirectToAction("AdminPanel", "Layout");
            }
            else
                return StatusCode(422);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public IActionResult GetEditBook(Guid? id)
        {
            var book = manager.GetBookById(id);
            ViewBag.Genres = manager.GetGenres().Select(x => x.Name);
            if (book != null)
                return View(book);
            else
                return NotFound();
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public IActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                if (!book.BookLink.Contains("pdf") && !book.CoverLink.Contains("jpg"))
                {
                    return StatusCode(422);
                }
                manager.EditBook(book);
                return RedirectToAction("AdminPanel", "Layout");
            }
            else
                return StatusCode(422);
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            manager.DeleteBook(id);
            return RedirectToAction("AdminPanel", "Layout");
        }
    }
}
