using PersonalMovieListApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PersonalMovieListApi.Data.Users
{
   public class UsersDbContext : IdentityDbContext<User>
   {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            :
            base(options)
       {
           
       }
   }
}