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

        public static async Task InitializeUsers(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
            
            //Seed Default User
            var defaultUser = new IdentityUser 
            {   UserName = Authorization.default_username, 
                Email = Authorization.default_email, 
                EmailConfirmed = true, 
                PhoneNumberConfirmed = true 
            };
            
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}