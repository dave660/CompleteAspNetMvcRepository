using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{

    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            
            _context = new ApplicationDbContext();
        }


        public ActionResult Test()
        {
            ViewBag.Movie = _context.Movies.Include("Genre").SingleOrDefault(m => m.Id == 1);

            return View();
        }
        // GET: Movies
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include("Genre").SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(MovieFormViewModel viewModel)
        {
            if (viewModel.Movie.Id == 0)
            {
                _context.Movies.Add(viewModel.Movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == viewModel.Movie.Id);

                movieInDb.DateAdded = viewModel.Movie.DateAdded;
                movieInDb.Name = viewModel.Movie.Name;
                movieInDb.ReleaseDate = viewModel.Movie.ReleaseDate;
                movieInDb.NumberInStock = viewModel.Movie.NumberInStock;
                movieInDb.GenreId = viewModel.Movie.GenreId;
                movieInDb.NumberAvailable = viewModel.Movie.NumberAvailable;
            }

            _context.SaveChanges();
           

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }
    }
}