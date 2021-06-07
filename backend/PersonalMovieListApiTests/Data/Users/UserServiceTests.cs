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
        private readonly string defaultPassword = "passw!!@3123OSAdord";
        private readonly string defaultEmail = "user@user.com";

        private readonly UsersDbContext _context;
        private readonly UserService _service;
        private readonly UserManager<IdentityUser> _manager;
        private readonly IdentityUser defaultUser;
        private bool userCreatedOnce = false;
        
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
                Key = "C1CF4B7EASDSADASD4F55CA4",
                Issuer = "Mock",
                Audience = "MockUser",
                DurationInMinutes = 3
            };

            IOptions<Jwt> jwtOptions = Options.Create(jwt);

            _service = new UserService(_manager, roles, jwtOptions);
        }

        private async void CreateDefaultUser()
        {
            await _manager.CreateAsync(defaultUser, defaultPassword);
        }

        [Fact]
        public async void GetTokenAsync_WhenCalledWithNull_ReturnsUnauthorizedAuthenticationModel()
        {
            AuthenticationModel model = await _service.GetTokenAsync(null);

            Assert.False(model.IsAuthenticated);
            Assert.Equal(null, model.Token);
        }

        [Fact]
        public async void GetTokenAsync_WhenCalledWithCorrectTokenModel_ReturnsAuthorizedAuthenticationModel()
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "user1",
                Email = defaultEmail
            };

            await _manager.CreateAsync(user, defaultPassword);

            TokenRequestModel requestModel = new TokenRequestModel
            {
                Email = defaultEmail,
                Password = defaultPassword                
            };

            AuthenticationModel authModel = await _service.GetTokenAsync(requestModel);

            Assert.True(authModel.IsAuthenticated);
            Assert.False(authModel.Token == null);
        }

        [Fact]
        public async void GetTokenAsync_WhenCalledWithInCorrectTokenModel_ReturnsUnauthorizedAuthenticationModel()
        {
            string email = "w@w.com";

            IdentityUser user = new IdentityUser
            {
                UserName = "user2",
                Email = email
            };

            await _manager.CreateAsync(user, defaultPassword);

            TokenRequestModel requestModel = new TokenRequestModel
            {
                Email = email,
                Password = defaultPassword + "111"               
            };

            AuthenticationModel authModel = await _service.GetTokenAsync(requestModel);

            Assert.False(authModel.IsAuthenticated);
            Assert.True(authModel.Token == null);
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