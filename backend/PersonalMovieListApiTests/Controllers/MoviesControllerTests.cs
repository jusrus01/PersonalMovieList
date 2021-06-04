using Xunit;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using System.Collections.Generic;
using PersonalMovieListApi.Controllers;
using AutoMapper;
using PersonalMovieListApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PersonalMovieListApi.Data.Users;

namespace PersonalMovieListApi.Tests
{
    public class MoviesControllerTests
    {
        private readonly MoviesController _controller;
        private readonly IMapper _mapper;
        private readonly IMoviesRepo _repo;
        private readonly IUserService _userService;

        public MoviesControllerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MoviesProfile()));

            _mapper = mapperConfig.CreateMapper();
            _repo = new MockMoviesRepo();
            _controller = new MoviesController(_repo, _mapper);
        }

        [Fact]
        public void Get_WhenCalledWithoutAuthToken_ReturnsBadRequestResult()
        {
            var badRequestResult = _controller.GetAllMovies();
            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        }

        [Fact]
        public void Get_WhenCalledWithAuthToken_ReturnsOnlySpecifiedUsersMovies()
        {
            //Assert.Equal(1,1);
        }

        [Fact]
        public void Get_WhenCalledWithBadAuthToken_ReturnsBadRequest()
        {
            //Assert.Equal(1,1);
        }

        [Fact]
        public void Get_WhenCalledWithAuthToken_ReturnsOkResult()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = GetMockJwtToken();

            //Assert.Equal(1,1);
        }

        private string GetMockJwtToken()
        {
            return "asdasda";
        }
    }
}