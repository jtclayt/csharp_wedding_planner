using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

using Session;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Weddings");
        }

        [HttpGet("weddings")]
        public IActionResult Weddings()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            return View("Weddings");
        }

        [HttpGet("weddings/new")]
        public ViewResult NewWedding()
        {
            return View("NewWedding");
        }

        private bool CheckUser()
        {
            ViewData["User"] = HttpContext.Session.Get<User>("user");
            return ViewData["User"] != null;
        }
    }
}
