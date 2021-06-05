using System.Collections.Generic;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using System.Linq;

namespace PersonalMovieListApi.Tests
{
    public class MockMoviesRepo : IMoviesRepo
    {
        IEnumerable<Movie> mockMovies;
        public void CreateMovie(Movie movie)
        {

        }

        public void DeleteMovie(Movie movie)
        {
            
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            mockMovies = new List<Movie>
            {
                new Movie
                {
                    Id = 0,
                    Title = "test1",
                    Comment = "test1",
                    Rating = 1,
                    OwnerUsername = "test1"
                },

                new Movie
                {
                    Id = 1,
                    Title = "test2",
                    Comment = "test2",
                    Rating = 1,
                    OwnerUsername = "test2"
                },

                new Movie
                {
                    Id = 2,
                    Title = "test3",
                    Comment = "test3",
                    Rating = 1,
                    OwnerUsername = "test2"
                },
            };

            return mockMovies;
        }

        public IEnumerable<Movie> GetAllMoviesByUserName(string username)
        {
            var movies = GetAllMovies();
            
            return movies.Where(movie => movie.OwnerUsername == username)
                .ToList();
        }

        public Movie GetMovieById(int id)
        {  
            mockMovies = GetAllMovies();
            try
            {
                Movie movie = mockMovies.Where(movie => movie.Id == id)
                    .FirstOrDefault();

                return movie;
            }
            catch
            {
                return null;
            }
        }

        public Movie GetMovieByTitle(string title)
        {
            return null;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateMovie(Movie movie)
        {
        }
    }
}