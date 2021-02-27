using FilmsCatalog.Interfaces;
using FilmsCatalog.Models;
using FilmsCatalog.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class MovieController : Controller
    {

        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("Movies/")]
        public IActionResult Index(int page = 1)
        {
            return View(_movieService.GetMovies(page));
        }

        [HttpGet]
        [Route("Movie/Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Movie/Add")]
        public IActionResult Add(MovieAddViewModel addVm)
        {
            bool addSucceed = false;
            if (ModelState.IsValid)
            {
                addSucceed = _movieService.Add(addVm);
            }

            ViewData["success"] = addSucceed;
            return View();
        }

        [HttpGet]
        [Route("Movie/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            string authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Movie movie = _movieService.GetByIdAndAuthor(id, authorId);
            if (movie == null) return NotFound(); 
            return View(new MovieEditViewModel { Movie = movie });
        }

        [HttpPost]
        [Route("Movie/Edit/{id}")]
        public IActionResult Edit(int id, MovieEditViewModel editVm)
        {
            bool editSucceed = false;

            if (ModelState.IsValid)
            {
                editSucceed = _movieService.Edit(id, editVm);
            }
            
            ViewData["success"] = editSucceed;
            string authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Movie movie = _movieService.GetByIdAndAuthor(id, authorId);
            if (movie == null) return NotFound();
            
            return View(new MovieEditViewModel { Movie = movie});
        }

        [HttpGet]
        [Route("Movie/View/{id}")]
        public IActionResult View(int id)
        {
            Movie movie = _movieService.GetById(id);
            if (movie == null) return NotFound();
            return View(new MovieViewModel { Movie = movie });
        }
    }
}
