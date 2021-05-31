using PersonalMovieListApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PersonalMovieListApi.Data.Users
{
   public class UsersDbContext : IdentityDbContext<IdentityUser>
   {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            :
            base(options)
       {
           
       }
   }
}