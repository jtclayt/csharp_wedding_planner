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
            User user = HttpContext.Session.Get<User>("user");
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(user);
        }
    }
}
