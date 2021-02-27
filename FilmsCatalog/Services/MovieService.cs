using FilmsCatalog.Data;
using FilmsCatalog.Interfaces;
using FilmsCatalog.Models;
using FilmsCatalog.ViewModels;
using FilmsCatalog.ViewModels.Movie;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace FilmsCatalog.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _appEnvironment;

        public MovieService(ApplicationDbContext dbContext,
            IWebHostEnvironment appEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _appEnvironment = appEnvironment;
        }

        public bool Add(MovieAddViewModel addViewModel)
        {
            if (addViewModel == null) return false;
           
            string posterImgPath = new MovieViewModel().SavePosterFile(addViewModel.Poster, _appEnvironment.WebRootPath);
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(posterImgPath)) return false;

            var movie = new Movie
            {
                Name = addViewModel.Name,
                Description = addViewModel.Description,
                ReleaseDate = addViewModel.ReleaseDate.Value,
                Director = addViewModel.Director,
                Poster = posterImgPath,
                AuthorId = userId
            };
            
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Edit(int id, MovieEditViewModel editViewModel)
        {
            if (id <= 0 || editViewModel == null) return false;
            string authorId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Movie movie = GetByIdAndAuthor(id, authorId);
            if (movie == null) return false;
            
            movie.Name = editViewModel.Name;
            movie.Description = editViewModel.Description;
            movie.Director = editViewModel.Director;
            movie.ReleaseDate = editViewModel.ReleaseDate.Value;
            
            if (editViewModel.Poster != null)
            {
                string newPosterFile = new MovieViewModel()
                                .EditPosterFile(editViewModel.Poster, _appEnvironment.WebRootPath, movie.Poster);
                if (string.IsNullOrEmpty(newPosterFile)) return false;
                movie.Poster = newPosterFile;
            }

            _dbContext.Movies.Update(movie);
            _dbContext.SaveChanges();
            return true;
        }

        public MovieViewModel GetMovies(int page)
        {
            IQueryable<Movie> movies = _dbContext.Movies
                .Include(movie => movie.Author);
            int perPage = 10;

            IQueryable<Movie> paginatedMovies = Paginate(movies, page, perPage);
            var pagingViewModel = new PagingViewModel(movies.Count(), page, perPage);
            return new MovieViewModel
            {
                PagingViewModel = pagingViewModel,
                Movies = paginatedMovies.ToList(),
            };
        }

        private IQueryable<Movie> Paginate(IQueryable<Movie> movies, int page, int perPage)
        {
            if (page <= 0) page = 1;
            return movies
                .OrderBy(movie => movie.Id)
                .Skip((page - 1) * perPage)
                .Take(perPage);
        }

        public Movie GetByIdAndAuthor(int id, string authorId)
        {
            if (id <= 0 || string.IsNullOrEmpty(authorId)) return null;
            return _dbContext.Movies.FirstOrDefault(movie => movie.Id == id && movie.AuthorId.Equals(authorId));
        }

        public Movie GetById(int id)
        {
            return _dbContext.Movies.FirstOrDefault(movie => movie.Id == id);
        }
    }
}
