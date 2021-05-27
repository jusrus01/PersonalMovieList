using System.Collections.Generic;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Data
{
    public interface IMoviesRepo
    {
        bool SaveChanges();
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        Movie GetMovieByTitle(string title);
        void CreateMovie(Movie movie);
    }
}