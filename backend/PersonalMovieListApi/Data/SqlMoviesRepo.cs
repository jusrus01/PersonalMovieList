using System.Collections.Generic;
using PersonalMovieListApi.Models;
using System.Linq;
using System;

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

        public void DeleteMovie(Movie movie) // should it reassign id's after deletion?
        {
            if(movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            _context.Movies.Remove(movie);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }

        public Movie GetMovieByTitle(string title)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}