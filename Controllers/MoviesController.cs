using Microsoft.AspNetCore.Mvc;
using Mission06_Blake.Models;

namespace Mission06_Blake.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private readonly ILogger<MoviesController> _logger; // Add a logger

        public MoviesController(MovieContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger; // Initialize logger
        }

        // Show the form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Process the form submission
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _logger.LogInformation("Form submitted.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");

                // 🔹 Log all validation errors to the console
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        _logger.LogError($"Validation error in '{error.Key}': {subError.ErrorMessage}");
                    }
                }

                return View(movie); // Return form with errors
            }

            _logger.LogInformation("Model is valid. Adding movie...");
            _context.Movies.Add(movie);
            _context.SaveChanges();
            _logger.LogInformation("Movie saved successfully!");

            return RedirectToAction("Index");
        }


        // Display all movies
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }
    }
}