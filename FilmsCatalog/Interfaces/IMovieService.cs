using FilmsCatalog.Models;
using FilmsCatalog.ViewModels.Movie;


namespace FilmsCatalog.Interfaces
{
    public interface IMovieService
    {
        public MovieViewModel GetMovies(int page);
        public bool Add(MovieAddViewModel addViewModel);
        public bool Edit(int id, MovieEditViewModel editViewModel);
        Movie GetByIdAndAuthor(int id, string authorId);
        Movie GetById(int id);
    }
}
