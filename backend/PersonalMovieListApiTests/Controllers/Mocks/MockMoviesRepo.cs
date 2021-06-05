using System.Collections.Generic;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using System.Linq;

namespace PersonalMovieListApi.Tests
{
        public class MockMoviesRepo : IMoviesRepo
    {
        public void CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var mockMovies = new List<Movie>
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

        public void UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}