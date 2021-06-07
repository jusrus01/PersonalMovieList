using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Data.Users;
using Xunit;
using System.Collections.Generic;
using PersonalMovieListApi.Models;
using PersonalMovieListApi.Settings;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace PersonalMovieListApiTests.Data.Users
{
    public class UserServiceTests
    {   
        private readonly UsersDbContext _context;
        private readonly UserService _service;
        private readonly UserManager<IdentityUser> _manager;
        
        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersMockDataBase")
                .Options;

            _context = new UsersDbContext(options);

            IUserStore<IdentityUser> store = new UserStore<IdentityUser>(_context);
            IRoleStore<IdentityRole> rolesStore = new RoleStore<IdentityRole>(_context);

            _manager = new UserManager<IdentityUser>(store, null, new PasswordHasher<IdentityUser>(),
                null, null, null, null, null, null);

            RoleManager<IdentityRole> roles = new RoleManager<IdentityRole>(rolesStore, null, null, null, null);
            Jwt jwt = new Jwt
            {
                Key = "C1CF4B7E4F55CA4",
                Issuer = "Mock",
                Audience = "MockUser",
                DurationInMinutes = 3
            };
            IOptions<Jwt> jwtOptions = Options.Create(jwt);

            _service = new UserService(_manager, roles, jwtOptions);
        }

        [Fact]
        public async void GetTokenAsync_WhenCalledWithNull_ReturnsUnauthorizedAuthenticationModel()
        {
            AuthenticationModel model = await _service.GetTokenAsync(null);

            Assert.False(model.IsAuthenticated);
        }

        [Fact]
        public void GetTokenAsync_WhenCalledWithCorrectTokenModel_ReturnsAuthorizedAuthenticationModel()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public void GetTokenAsync_WhenCalledWithInCorrectTokenModel_ReturnsUnauthorizedAuthenticationModel()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public void RegisterAsync_WhenCalledWithExistingUser_DoesNotCreateUser()
        {

        }

        [Fact]
        public void RegisterAsync_WhenCalledWithNull_DoesNotThrow()
        {

        }

        [Fact]
        public void RegisterAsync_WhenCalledCorrectUser_CreatesUser()
        {
            
        }
    }
}