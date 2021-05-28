using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MoviesDbContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        Rating = 1,
                        Comment = "None"
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        Rating = 2,
                        Comment = "None"
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        Rating = 3,
                        Comment = "None"
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        Rating = 4,
                        Comment = "None"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}