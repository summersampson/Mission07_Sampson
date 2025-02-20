using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission07_Sampson.Models;

namespace Mission07_Sampson.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // Method to display the Add Movie page with category list
        [HttpGet]
        public IActionResult AddMovie()
        {
            var categories = _context.Categories.ToList(); // Fetch categories from DB

            if (categories == null || !categories.Any())
            {
                categories = new List<Category>(); // Avoid null errors
            }

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        // Method to handle Add Movie form submission
        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("MovieList"); // Redirect after submission
            }

            // If validation fails, reload categories to prevent dropdown from breaking
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View(movie);
        }

        // Method to display the movie list
        public IActionResult MovieList()
        {
            var movies = _context.Movies
                .Include(m => m.Category) // Ensure Category data is included
                .ToList();

            return View(movies);
        }

        // Method to display the Edit Movie page
        public IActionResult EditMovie(int id)
        {
            var movie = _context.Movies.Include(m => m.Category).FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound(); // Returns a 404 if the movie isn't found
            }

            // Make sure ViewBag is populated with categories for the dropdown
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", movie.CategoryId);

            return View(movie); // Return the view with the model
        }


        // Method to handle Edit Movie form submission
        [HttpPost]
        public IActionResult EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
                return RedirectToAction("MovieList"); // Redirect back to MovieList after editing
            }

            // If validation fails, reload categories for dropdown
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", movie.CategoryId);

            return View(movie);
        }

        // Method to delete a movie
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }

            return RedirectToAction("MovieList"); // Redirect back to MovieList after deletion
        }
    }
}
