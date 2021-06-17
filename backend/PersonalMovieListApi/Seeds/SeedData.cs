using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using PersonalMovieListApi.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MoviesDbContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                context.Movies.AddRange(
                    new MovieModel
                    {
                        Title = "When Harry Met Sally",
                        Rating = 1,
                        Comment = "None",
                        OwnerUsername = "test"
                    },

                    new MovieModel
                    {
                        Title = "Ghostbusters ",
                        Rating = 2,
                        Comment = "None",
                        OwnerUsername = "test"
                    },

                    new MovieModel
                    {
                        Title = "Ghostbusters 2",
                        Rating = 3,
                        Comment = "None",
                        OwnerUsername = "test2"
                    },

                    new MovieModel
                    {
                        Title = "Rio Bravo",
                        Rating = 4,
                        Comment = "None",
                        OwnerUsername = "test2"
                    }
                );
                context.SaveChanges();
            }
        }

        public static async Task InitializeUsers(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if(userManager.Users.Any())
            {
                return;
            }

            var defaultRole = new IdentityRole("User");

            await roleManager.CreateAsync(defaultRole);
            
            var defaultUser = new IdentityUser
            {   UserName = "default",
                Email = "default@default",
                EmailConfirmed = true, 
                PhoneNumberConfirmed = true 
            };

            await userManager.CreateAsync(defaultUser, "default");
            await userManager.AddToRoleAsync(defaultUser, defaultRole.ToString());
        }
    }
}