using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Session;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private Context _db;

        public HomeController(Context context)
        {
            _db = context;
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

            WeddingsWrapper data = new WeddingsWrapper();
            int userId = ((User) ViewData["user"]).UserId;
            data.User = _db.Users
                .Include(u => u.Rsvps)
                .Include(u => u.WeddingsCreated)
                .SingleOrDefault(u => u.UserId == userId);
            data.AllWeddings = _db.Weddings
                .Include(w => w.Attendees)
                .OrderBy(w => w.Date)
                .ToArray();
            return View("Weddings", data);
        }

        [HttpPost("weddings")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            User creator = HttpContext.Session.Get<User>("user");
            if (creator == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                newWedding.UserId = creator.UserId;
                _db.Add(newWedding);
                _db.SaveChanges();
                return RedirectToAction("Weddings");
            }
            return NewWedding();
        }

        [HttpGet("weddings/new")]
        public IActionResult NewWedding()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            return View("NewWedding");
        }

        [HttpGet("weddings/{id}")]
        public IActionResult DisplayWedding(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            Wedding wedding = _db.Weddings
                .Include(w => w.Attendees)
                .ThenInclude(a => a.Attendee)
                .SingleOrDefault(w => w.WeddingId == id);
            return View("DisplayWedding", wedding);
        }

        [HttpGet("weddings/{id}/rsvp")]
        public IActionResult ToggleRsvp(int id)
        {
            User user = HttpContext.Session.Get<User>("user");
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            Rsvp rsvp = _db.Rsvps
                    .SingleOrDefault(r => r.UserId == user.UserId && r.WeddingId == id);
            if (rsvp == null)
            {
                Rsvp newRsvp = new Rsvp();
                newRsvp.UserId = user.UserId;
                newRsvp.WeddingId = id;
                _db.Add(newRsvp);
            }
            else
            {
                _db.Remove(rsvp);
            }
            _db.SaveChanges();
            return RedirectToAction("Weddings");
        }

        [HttpGet("weddings/{id}/remove")]
        public IActionResult DeleteWedding(int id)
        {
            User user = HttpContext.Session.Get<User>("user");
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            Wedding wedding = _db.Weddings
                .SingleOrDefault(w => w.WeddingId == id);
            if (wedding.UserId == user.UserId)
            {
                _db.Remove(wedding);
                _db.SaveChanges();
            }
            return RedirectToAction("Weddings");
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
