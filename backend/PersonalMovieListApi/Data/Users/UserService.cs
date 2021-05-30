using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PersonalMovieListApi.Models;
using PersonalMovieListApi.Settings;

namespace PersonalMovieListApi.Data.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Jwt _jwt;

        public UserService(UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, IOptions<Jwt> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
    }
}