using bLibrary.API;
using bLibrary.Models;
using bLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace bLibrary.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet, ValidateAntiForgeryToken]
        public IActionResult Main()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}