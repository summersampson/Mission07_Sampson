using Microsoft.AspNetCore.Mvc;
using Mission07_Sampson.Models;

namespace Mission07_Sampson.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDbContext _context;

        public HomeController(MovieDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public IActionResult AddMovie()
        {
            var categories = _context.Categories.OrderBy(c => c.CategoryName).ToList();

            if (categories == null || !categories.Any())
            {
                categories = new List<Category>(); // Ensure it's never null
            }

            ViewBag.Categories = categories;
            return View();
        }


        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(movie);
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

    }
}
