using System.Collections.Generic;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using System.Linq;

namespace PersonalMovieListApi.Tests
{
    public class MockMoviesRepo : IMoviesRepo
    {
        IEnumerable<MovieModel> mockMovies;

        public void CreateMovie(MovieModel movie)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteMovie(MovieModel movie)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            mockMovies = new List<MovieModel>
            {
                new MovieModel
                {
                    Id = 0,
                    Title = "test1",
                    Comment = "test1",
                    Rating = 1,
                    OwnerUsername = "test1"
                },

                new MovieModel
                {
                    Id = 1,
                    Title = "test2",
                    Comment = "test2",
                    Rating = 1,
                    OwnerUsername = "test2"
                },

                new MovieModel
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

        public IEnumerable<MovieModel> GetAllMoviesByUserName(string username)
        {
            var movies = GetAllMovies();
            
            return movies.Where(movie => movie.OwnerUsername == username)
                .ToList();
        }

        public MovieModel GetMovieById(int id)
        {  
            mockMovies = GetAllMovies();
            try
            {
                var movie = mockMovies.Where(movie => movie.Id == id)
                    .FirstOrDefault();

                return movie;
            }
            catch
            {
                return null;
            }
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateMovie(MovieModel movie)
        {
            throw new System.NotImplementedException();
        }
    }
}