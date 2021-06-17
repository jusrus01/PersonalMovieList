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

        public void CreateMovie(MovieModel movie)
        {
            if(movie == null)
            {
                throw new ArgumentNullException();
            }

            _context.Movies.Add(movie);
        }

        public void DeleteMovie(MovieModel movie)
        {
            if(movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            _context.Movies.Remove(movie);
        }

        public IEnumerable<MovieModel> GetAllMoviesByUserName(string username)
        {
            return _context.Movies.Where(movie => movie.OwnerUsername == username).ToList();
        }

        public MovieModel GetMovieById(int id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateMovie(MovieModel movie)
        {
            if(movie == null)
            {
                throw new ArgumentNullException();
            }

            _context.Movies.Update(movie);
        }
    }
}