using System.Collections.Generic;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Data
{
    public class MockMoviesRepo : IMoviesRepo
    {
        public void CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Hello",
                    Rating = 1,
                    Comment = "World"
                },

                new Movie
                {
                    Id = 2,
                    Title = "World",
                    Rating = 1,
                    Comment = "Hello"
                }
            };

            return movies;
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