using System.Collections.Generic;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Data
{
    public interface IMoviesRepo
    {
        bool SaveChanges();
        IEnumerable<Movie> GetAllMovies();
        IEnumerable<Movie> GetAllMoviesByUserName(string username);
        Movie GetMovieById(int id);
        Movie GetMovieByTitle(string title);
        void CreateMovie(Movie movie);
        void DeleteMovie(Movie movie);
        void UpdateMovie(Movie movie);
    }
}