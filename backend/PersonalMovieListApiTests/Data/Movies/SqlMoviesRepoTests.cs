using Microsoft.EntityFrameworkCore;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using Xunit;
using System;
using System.Linq;

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
            _context.Movies.Add(new Movie
            {
                Title = "test1",
                Comment = "test1",
                Rating = 1,
                OwnerUsername = "test1"
            });    

            _context.Movies.Add(new Movie
            {
                Title = "test2",
                Comment = "test2",
                Rating = 1,
                OwnerUsername = "test2"
            });    

            _context.Movies.Add(new Movie
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
            Movie movieNull = null;

            Assert.Throws<ArgumentNullException>(() => { _repo.CreateMovie(movieNull); _context.Dispose(); });
        }

        [Fact]
        public void CreateMovie_WhenCalledWithMovie_AddsMovieToDatabase()
        {
            string title = "test";
            string comment = "test";
            int rating = 1;
            string owner = "owner";

            Movie createdMovie = new Movie
            {
                Title = title,
                Comment = comment,
                Rating = rating,
                OwnerUsername = owner
            };

            _repo.CreateMovie(createdMovie);
            _repo.SaveChanges();

            Movie actualMovie = _context.Movies.Where(movie => 
                movie.Comment == comment &&
                movie.OwnerUsername == owner &&
                movie.Rating == rating &&
                movie.Title == title).SingleOrDefault();

            _context.Dispose();

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

            Movie deleteMovie = new Movie
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

            Movie movieShouldBeNull  = _context.Movies.Where(movie => 
                movie.Comment == comment &&
                movie.OwnerUsername == owner &&
                movie.Rating == rating &&
                movie.Title == title).SingleOrDefault();

            _context.Dispose();

            Assert.Equal(null, movieShouldBeNull);
        }

        [Fact]
        public void DeleteMovie_WhenCalledWithNonExistingMovie_DoesNotThrow()
        {
            string title = "test";
            string comment = "test";
            int rating = 1;
            string owner = "owner";

            Movie deleteMovie = new Movie
            {
                Title = title,
                Comment = comment,
                Rating = rating,
                OwnerUsername = owner
            };

            _repo.DeleteMovie(deleteMovie);
            _context.Dispose();

            Assert.True(true);
        }

        [Fact]
        public void DeleteMovie_WhenCalledWithNull_ThrowsArgumentNullException()
        {
            Movie movieNull = null;

            Assert.Throws<ArgumentNullException>(() => { _repo.DeleteMovie(movieNull); _context.Dispose(); });
        }

        [Fact]
        public void GetAllMoviesByUserName_WhenCalledWithUsername_ReturnsCorrectMovies()
        {
            string owner = "test2";
            bool correctMovies = true;

            var movies = _repo.GetAllMoviesByUserName(owner);

            foreach(var movie in movies)
                if(movie.OwnerUsername != owner)
                {
                    correctMovies = false;
                    break;
                }

            _context.Dispose();
            
            Assert.True(correctMovies);
        }

        [Fact]
        public void GetAllMoviesByUserName_WhenCalledWithNonExistingUsername_ReturnsEmptyList()
        {
            string owner = "such owner does not exist";

            var movies = _repo.GetAllMoviesByUserName(owner);

            _context.Dispose();

            Assert.Equal(0, movies.Count());
        }

        [Fact]
        public void GetAllMoviesByUserName_WhenCalledWithNull_DoesNotThrow()
        {
            _repo.GetAllMoviesByUserName(null);
            _context.Dispose();

            Assert.True(true);
        }

        [Fact]
        public void GetMovieById_WhenCalledWithNonExistingId_DoesNotThrow()
        {
            
        }

        [Fact]
        public void GetMovieById_WhenCalledWithCorredId_ReturnsMovie()
        {
            
        }

        [Fact]
        public void GetMovieById_WhenCalledWithNegativeId_DoesNotThrow()
        {

        }

        [Fact]
        public void UpdateMovie_WhenCalledWithNull_ThrowsArgumentException()
        {

        }

        [Fact]
        public void UpdateMovie_WhenCalledWithCorrectMovie_UpdatesMovie()
        {
            
        }

        [Fact]
        public void UpdateMovie_WhenCalledWithNotCorrectMovie_DoesNotThrow()
        {
            
        }
    }
}