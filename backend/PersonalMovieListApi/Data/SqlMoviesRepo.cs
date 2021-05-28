using System.Collections.Generic;
using PersonalMovieListApi.Models;
using System.Linq;

namespace PersonalMovieListApi.Data
{
    public class SqlMoviesRepo : IMoviesRepo
    {
        private readonly MoviesDbContext _context;

        public SqlMoviesRepo(MoviesDbContext context)
        {
            _context = context;
        }

        public void CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Movie GetMovieByTitle(string title)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}