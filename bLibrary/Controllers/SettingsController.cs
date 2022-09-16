using bLibrary.API;
using bLibraryAPI.Models;
using bLibraryAPI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bLibrary.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ClientManager manager;
        public SettingsController()
        {
            manager = new ClientManager();
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            AdminSettingsModel model = new AdminSettingsModel
            {
                Books = manager.GetBooks(),
                Users = manager.GetUsers()
            };

            return View(model);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public IActionResult UserSettings(string id)
        {
            User user = manager.GetUserById(id);
            if (user != null)
                return View(user);
            else
                return NotFound();
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public IActionResult EditUserSettings(User user)
        {
            if (ModelState.IsValid)
            {
                manager.EditUser(user);
                return RedirectToAction("Main", "Home");
            }
            else
                return StatusCode(422);
        }
    }
    internal class AdminSettingsModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
