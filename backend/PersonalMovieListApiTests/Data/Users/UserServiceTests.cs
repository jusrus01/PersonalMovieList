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
using System.Linq;
using System;

namespace PersonalMovieListApiTests.Data.Users
{
    public class UserServiceTests
    {   
        private readonly string defaultPassword = "passw!!@3123OSAdord";
        private readonly string defaultEmail = "user@user.com";

        private readonly UsersDbContext _context;
        private readonly UserService _service;
        private readonly UserManager<IdentityUser> _manager;
        private readonly RoleManager<IdentityRole> _roleManager;
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

            _roleManager = new RoleManager<IdentityRole>(rolesStore, null, null, null, null);


            Jwt jwt = new Jwt
            {
                Key = "C1CF4B7EASDSADASD4F55CA4",
                Issuer = "Mock",
                Audience = "MockUser",
                DurationInMinutes = 3
            };

            IOptions<Jwt> jwtOptions = Options.Create(jwt);

            _service = new UserService(_manager, _roleManager, jwtOptions);
        }

        private async void CreateDefaultUser()
        {
            await _manager.CreateAsync(defaultUser, defaultPassword);
        }

        [Fact]
        public async void GetTokenAsync_WhenCalledWithNull_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetTokenAsync(null));
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
        public async void RegisterAsync_WhenCalledWithExistingUser_DoesNotCreateUser()
        {
            string username = "user321";
            string email = "a@a.com";

            IdentityUser existingUser = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            await _manager.CreateAsync(existingUser, defaultPassword);
            
            RegisterModel model = new RegisterModel
            {
                Username = username,
                Email = email,
                Password = defaultPassword
            };

            string msg = await _service.RegisterAsync(model);

            var foundUsers = _manager.Users.Where(user =>
                user.UserName == existingUser.UserName &&
                user.Email == existingUser.Email);


            Assert.Equal(1, foundUsers.Count());
        }

        [Fact]
        public async void RegisterAsync_WhenCalledWithNull_ThrowsArgumentNullException()
        {
           await Assert.ThrowsAsync<ArgumentNullException>(() => _service.RegisterAsync(null));
        }

        [Fact]
        public async void RegisterAsync_WhenCalledCorrectUser_CreatesUser()
        {
            string username = "user321231";
            string email = "a@2a.com";
            await _roleManager.CreateAsync(new IdentityRole("User"));
            
            RegisterModel model = new RegisterModel
            {
                Username = username,
                Email = email,
                Password = defaultPassword
            };

            string msg = await _service.RegisterAsync(model);

            var foundUsers = _manager.Users.Where(user =>
                user.UserName == model.Username &&
                user.Email == model.Email);


            Assert.Equal(1, foundUsers.Count());
        }
    }
}