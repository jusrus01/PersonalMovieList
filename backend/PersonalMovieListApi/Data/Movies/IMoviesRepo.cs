using System.Collections.Generic;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Data
{
    public interface IMoviesRepo
    {
        bool SaveChanges();
        IEnumerable<MovieModel> GetAllMoviesByUserName(string username);
        MovieModel GetMovieById(int id);
        void CreateMovie(MovieModel movie);
        void DeleteMovie(MovieModel movie);
        void UpdateMovie(MovieModel movie);
    }
}