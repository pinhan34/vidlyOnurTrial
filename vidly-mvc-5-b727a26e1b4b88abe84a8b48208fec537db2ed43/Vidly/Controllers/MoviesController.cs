using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
                _dbContext = new ApplicationDbContext();
        }

        public ViewResult Index()
        {
            var movies = _dbContext.Movies.Include(m => m.Genre).ToList();

            return View(movies);    
        }

        public ActionResult New()
        {
            var genres = _dbContext.Genres.ToList();

            var movieGenresModel = new MovieGenresViewModel()
            {
                Genres = genres
            };

            return View("MovieForm",movieGenresModel);
        }


        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModelMovie = new MovieGenresViewModel(movie)
                {
                    Genres = _dbContext.Genres.ToList(),
                };
                return View("MovieForm", viewModelMovie);
            }

            if (movie.Id == 0)
            {
                _dbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
            }

            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Movies");
            
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }


        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}

        // GET: Movies/Details
        //public ActionResult Details(int id)
        //{

        //    var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

        //    if (movie == null)
        //        return HttpNotFound();

        //    return View(movie);

        //}

        public ActionResult Edit(int id)
        {
            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var modelMovieGenres = new MovieGenresViewModel()
            {
                Id = movie.Id,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                NumberInStock = movie.NumberInStock,
                GenreId = movie.GenreId,
                Genres = _dbContext.Genres.ToList()
            };
            return View("MovieForm", modelMovieGenres);
        }
    }
}