using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalMovieListApi.Controllers;
using PersonalMovieListApi.Data.Users;
using PersonalMovieListApi.Models;
using PersonalMovieListApi.Settings;
using PersonalMovieListApi.Tests;
using Xunit;

namespace PersonalMovieListApiTests.Tests
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        public UsersControllerTests()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersMockDataBase2")
                .Options;

            var _context = new UsersDbContext(options);

            IUserStore<IdentityUser> store = new UserStore<IdentityUser>(_context);
            IRoleStore<IdentityRole> rolesStore = new RoleStore<IdentityRole>(_context);

            var _manager = new UserManager<IdentityUser>(store, null, new PasswordHasher<IdentityUser>(),
                null, null, null, null, null, null);

            var _roleManager = new RoleManager<IdentityRole>(rolesStore, null, null, null, null);
            _roleManager.CreateAsync(new IdentityRole("User"));

            Jwt jwt = new Jwt
            {
                Key = "C1CF4B7EASDSADASD4F55CA4",
                Issuer = "Mock",
                Audience = "MockUser",
                DurationInMinutes = 3
            };

            IOptions<Jwt> jwtOptions = Options.Create(jwt);

            var _service = new UserService(_manager, _roleManager, jwtOptions);

            _controller = new UsersController(_service);
        }

        [Fact]
        public async void Post_RegisterAsync_WhenCalledWithNull_ReturnsBadRequest()
        {
            if(_controller == null)
            {
                
                return;
            }

            var badRequest = await _controller.RegisterAsync(null);

            Assert.IsType<BadRequestResult>(badRequest);
        }

        [Fact]
        public async void Post_RegisterAsync_WhenCalledWithCorrectModel_ReturnsOk()
        {
            RegisterModel model = new RegisterModel
            {
                Username = "username",
                Password = "password",
                Email = "email@email.com"
            };

            var okResult = await _controller.RegisterAsync(model);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async void Post_GetTokenAsync_WhenCalledWithNull_ReturnsBadRequest()
        {
            var badRequest = await _controller.GetTokenAsync(null);

            Assert.IsType<BadRequestResult>(badRequest);
        }

        [Fact]
        public async void Post_GetTokenAsync_WhenCalledCorrectModel_ReturnsOk()
        {
            RegisterModel model = new RegisterModel
            {
                Username = "username",
                Password = "password",
                Email = "email@email.com"
            };

            await _controller.RegisterAsync(model);

            TokenRequestModel token = new TokenRequestModel
            {
                Email = model.Email,
                Password = model.Password
            };
            
            var okResult = await _controller.GetTokenAsync(token);

            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}