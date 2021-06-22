using Microsoft.EntityFrameworkCore;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using Xunit;
using System;
using System.Linq;
using PersonalMovieListApi.Dtos;

namespace PersonalMovieListApiTests.Data.Movies
{
    public class SqlMoviesRepoTests
    {
        private readonly SqlMoviesRepo _repo;
        private readonly MoviesDbContext _context;
        public SqlMoviesRepoTests()
        {  
            var options = new DbContextOptionsBuilder<MoviesDbContext>()
                .UseInMemoryDatabase(databaseName: "MoviesMockDataBase")
                .Options;


            _context = new MoviesDbContext(options);

            _context.Movies.Add(new MovieModel
            {
                Title = "test1",
                Comment = "test1",
                Rating = 1,
                OwnerUsername = "test1"
            });    

            _context.Movies.Add(new MovieModel
            {
                Title = "test2",
                Comment = "test2",
                Rating = 1,
                OwnerUsername = "test2"
            });    

            _context.Movies.Add(new MovieModel
            {
                Title = "test3",
                Comment = "test3",
                Rating = 1,
                OwnerUsername = "test2"
            });           

            _repo = new SqlMoviesRepo(_context);
        }

        [Fact]
        public void CreateMovie_WhenCalledWithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.CreateMovie(null));
        }

        [Fact]
        public void CreateMovie_WhenCalledWithMovie_AddsMovieToDatabase()
        {
            string title = "test";
            string comment = "test";
            int rating = 1;
            string owner = "owner";

            var createdMovie = new MovieModel
            {
                Title = title,
                Comment = comment,
                Rating = rating,
                OwnerUsername = owner
            };

            _repo.CreateMovie(createdMovie);
            _repo.SaveChanges();

            var actualMovie = _context.Movies.Where(movie => 
                movie.Comment == comment &&
                movie.OwnerUsername == owner &&
                movie.Rating == rating &&
                movie.Title == title).SingleOrDefault();

            Assert.Equal(title, actualMovie.Title);
            Assert.Equal(comment, actualMovie.Comment);
            Assert.Equal(rating, actualMovie.Rating);
            Assert.Equal(owner, actualMovie.OwnerUsername);
        }

        [Fact]
        public void DeleteMovie_WhenCalledWithMovie_DeletesMovie()
        {
            string title = "test1";
            string comment = "test1";
            int rating = 1;
            string owner = "owner";

            var deleteMovie = new MovieModel
            {
                Title = title,
                Comment = comment,
                Rating = rating,
                OwnerUsername = owner
            };

            _repo.CreateMovie(deleteMovie);
            _repo.SaveChanges();

            _repo.DeleteMovie(deleteMovie);
            _repo.SaveChanges();

            var movieShouldBeNull  = _context.Movies.Where(movie => 
                movie.Comment == comment &&
                movie.OwnerUsername == owner &&
                movie.Rating == rating &&
                movie.Title == title).SingleOrDefault();

            Assert.Null(movieShouldBeNull);
        }

        [Fact]
        public void DeleteMovie_WhenCalledWithNonExistingMovie_DoesNotThrow()
        {
            string title = "test";
            string comment = "test";
            int rating = 1;
            string owner = "owner";

            var deleteMovie = new MovieModel
            {
                Title = title,
                Comment = comment,
                Rating = rating,
                OwnerUsername = owner
            };

            _repo.DeleteMovie(deleteMovie);

            Assert.True(true);
        }

        [Fact]
        public void DeleteMovie_WhenCalledWithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.DeleteMovie(null));
        }

        [Fact]
        public void GetAllMoviesByUserName_WhenCalledWithUsername_ReturnsCorrectMovies()
        {
            string owner = "test2";
            bool correctMovies = true;

            var movies = _repo.GetAllMoviesByUserName(owner);

            foreach(var movie in movies)
            {
                if(movie.OwnerUsername != owner)
                {
                    correctMovies = false;
                    break;
                }
            }
            
            Assert.True(correctMovies);
        }

        [Fact]
        public void GetAllMoviesByUserName_WhenCalledWithNonExistingUsername_ReturnsEmptyList()
        {
            string owner = "such owner does not exist";

            var movies = _repo.GetAllMoviesByUserName(owner);

            Assert.Equal(0, movies.Count());
        }

        [Fact]
        public void GetAllMoviesByUserName_WhenCalledWithNull_DoesNotThrow()
        {
            _repo.GetAllMoviesByUserName(null);

            Assert.True(true);
        }

        [Fact]
        public void GetMovieById_WhenCalledWithNonExistingId_DoesNotThrow()
        {
            var foundMovie = _repo.GetMovieById(-10);

            Assert.Null(foundMovie);
        }

        [Fact]
        public void GetMovieById_WhenCalledWithCorrectId_ReturnsMovie()
        {
            var sampleMovie = _context.Movies.Last();
            int id = sampleMovie.Id;

            var foundMovie = _repo.GetMovieById(id);

            Assert.Equal(sampleMovie.Title, foundMovie.Title);
            Assert.Equal(sampleMovie.Comment, foundMovie.Comment);
            Assert.Equal(sampleMovie.Id, foundMovie.Id);
            Assert.Equal(sampleMovie.OwnerUsername, foundMovie.OwnerUsername);
            Assert.Equal(sampleMovie.Rating, foundMovie.Rating);
        }

        [Fact]
        public void UpdateMovie_WhenCalledWithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.UpdateMovie(null));
        }

        [Fact]
        public void UpdateMovie_WhenCalledWithCorrectMovie_UpdatesMovie()
        {
            string title = "new title";
            string comment = "new comment";
            int rating = 4;

            var sampleMovie = _context.Movies.First();
            sampleMovie.Title = title;
            sampleMovie.Comment = comment;
            sampleMovie.Rating = rating;

            _repo.UpdateMovie(sampleMovie);
            _repo.SaveChanges();

            var updatedMovie = _context.Movies.First();

            Assert.Equal(title, updatedMovie.Title);
            Assert.Equal(comment, updatedMovie.Comment);
            Assert.Equal(rating, updatedMovie.Rating);
        }

        [Fact]
        public void UpdateMovie_WhenCalledWithNotCorrectMovie_DoesNotThrow()
        {
            var randomMovie = new MovieModel
            {
                Title = "random",
                Comment = "random",
                Rating = 1
            };

            _repo.UpdateMovie(randomMovie);
            _repo.SaveChanges();

            Assert.True(true);
        }
    }
}