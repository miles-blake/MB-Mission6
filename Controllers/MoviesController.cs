using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission06_Blake.Models;

namespace Mission06_Blake.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(MovieContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // --------------------------------------
        // CREATE
        // --------------------------------------
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _logger.LogInformation("Form submitted.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");

                // More detailed per‐key error logging
                foreach (var state in ModelState)
                {
                    var fieldKey = state.Key;             // e.g. "CategoryId" or "Category"
                    var fieldErrors = state.Value.Errors; 
                    if (fieldErrors.Count > 0)
                    {
                        foreach (var err in fieldErrors)
                        {
                            _logger.LogError($"Key '{fieldKey}' had error: {err.ErrorMessage}");
                        }
                    }
                }

                ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", movie.CategoryId);
                return View(movie);
            }


            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // --------------------------------------
        // READ (INDEX)
        // --------------------------------------
        public IActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Category).ToList();
            return View(movies);
        }

        // --------------------------------------
        // EDIT
        // --------------------------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", movie.CategoryId);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", movie.CategoryId);
                return View(movie);
            }

            _context.Movies.Update(movie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // --------------------------------------
// DELETE
// --------------------------------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .FirstOrDefault(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Renders Delete.cshtml, which asks the user “Are you sure?”
            return View(movie);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // Actually remove from DB
            var movie = _context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
