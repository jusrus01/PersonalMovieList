using Microsoft.EntityFrameworkCore;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;

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

            using(_context = new MoviesDbContext(options))
            {
                _context.Movies.Add(new Movie
                {
                    Id = 0,
                    Title = "test1",
                    Comment = "test1",
                    Rating = 1,
                    OwnerUsername = "test1"
                });

                _context.Movies.Add(new Movie
                {
                    Id = 1,
                    Title = "test2",
                    Comment = "test2",
                    Rating = 1,
                    OwnerUsername = "test2"
                });

                _context.Movies.Add(new Movie
                {
                    Id = 2,
                    Title = "test3",
                    Comment = "test3",
                    Rating = 1,
                    OwnerUsername = "test2"
                });
            }

            _repo = new SqlMoviesRepo(_context);
        }

        
    }
}