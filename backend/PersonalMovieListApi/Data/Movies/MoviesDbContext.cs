using PersonalMovieListApi.Models;
using Microsoft.EntityFrameworkCore;


namespace PersonalMovieListApi.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            :
            base(options)
        {
            
        }

        public DbSet<MovieModel> Movies { get; set; }
    }
}