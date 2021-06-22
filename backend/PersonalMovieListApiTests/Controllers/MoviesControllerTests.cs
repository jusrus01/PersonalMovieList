using Xunit;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;
using System.Collections.Generic;
using System.Collections;
using PersonalMovieListApi.Controllers;
using AutoMapper;
using PersonalMovieListApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PersonalMovieListApi.Data.Users;
using System.Threading.Tasks;
using System.Linq;
using System;
using PersonalMovieListApi.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
            _userService = new MockUsersService();
        }

        [Fact]
        public void Get_WhenCalledWithoutAuthToken_ReturnsBadRequestResult()
        {
            var badRequestResult = _controller.GetAllMovies();
            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        }

        [Fact]
        public void Get_WhenCalledWithBadAuthToken_ReturnsBadRequest()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer some random string";

            var badRequestResult = _controller.GetAllMovies();

            Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        }

        [Fact]
        public void Get_WhenCalledWithAuthToken_ReturnsOkResult()
        {
            SetUpCorrectAuthTokenForController();
                        
            var okResult = _controller.GetAllMovies();

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Delete_WhenCalledWithoutAuthToken_ReturnsBadRequest()
        {
            var badRequest = _controller.DeleteMovie(2);
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void Delete_WhenCalledWithBadAuthToken_ReturnsBadRequest()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer some random string";

            var badRequest = _controller.DeleteMovie(1);

            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void Delete_WhenCalledWithAuthTokenButBadMovieParams_ReturnsNotFound()
        {
            SetUpCorrectAuthTokenForController();

            var notFound = _controller.DeleteMovie(-10);

            Assert.IsType<NotFoundResult>(notFound);
        }


        [Fact]
        public void Delete_WhenCalledWithAuthToken_ReturnsNoContent()
        {
            SetUpCorrectAuthTokenForController();

            var noContent = _controller.DeleteMovie(1);

            Assert.IsType<NoContentResult>(noContent);
        }

        [Fact]
        public void Delete_WhenCalledWithAuthTokenButBadOwner_ReturnsBadRequest()
        {
            SetUpCorrectAuthTokenForController();

            var badRequest = _controller.DeleteMovie(0);

            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void Put_WhenCalledWithAuthToken_ReturnsNoContent()
        {
            SetUpCorrectAuthTokenForController();
            MovieUpdateDto updateDto = new MovieUpdateDto
            {
                Title = "temp",
                Comment = "temp",
                Rating = 1
            };

            var noContent = _controller.UpdateMovie(1, updateDto);

            Assert.IsType<NoContentResult>(noContent);
        }

        [Fact]
        public void Put_WhenCalledWithNoAuthToken_ReturnsBadRequest()
        {
            MovieUpdateDto updateDto = new MovieUpdateDto
            {
                Title = "temp",
                Comment = "temp",
                Rating = 1
            };

            var badRequest = _controller.UpdateMovie(1, updateDto);

            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void Put_WhenCalledWithBadParams_ReturnsBadRequest()
        {
            SetUpCorrectAuthTokenForController();
            MovieUpdateDto updateDto = new MovieUpdateDto
            {
                Title = "temp",
                Comment = "temp",
                Rating = 1
            };

            var notFound = _controller.UpdateMovie(-10, updateDto);

            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public void Put_WhenCalledOnNotOwnedMovie_ReturnsBadRequest()
        {
            SetUpCorrectAuthTokenForController();
            MovieUpdateDto updateDto = new MovieUpdateDto
            {
                Title = "temp",
                Comment = "temp",
                Rating = 1
            };

            var badRequest = _controller.UpdateMovie(0, updateDto);

            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        // NOTE: Does not pass this test, because the model state validation is only triggered during runtime.
        [Fact]
        public void Post_WhenCalledWithAuthToken_ReturnsCreatedAtRouteResult()
        {
            SetUpCorrectAuthTokenForController();
            string title = "test";
            string comment = "test";
            int rating = 1;

            MovieCreateDto createdMovie = new MovieCreateDto
            {
                Title = title,
                Comment = comment,
                Rating = rating
            };

            var createdAtRoute = _controller.CreateMovie(createdMovie);

            Assert.IsType<CreatedAtRouteResult>(createdAtRoute.Result);
        }

        [Fact]
        public void Post_WhenCalledWithAuthTokenAndBadParams_ReturnsBadRequest()
        {
            SetUpCorrectAuthTokenForController();
            MovieCreateDto createdMovie = null;

            var badRequest = _controller.CreateMovie(createdMovie);

            Assert.IsType<BadRequestObjectResult>(badRequest.Result);
        }

        [Fact]
        public void Post_WhenCalledWithNoAuthToken_ReturnsBadRequest()
        {
            string title = "test";
            string comment = "test";
            int rating = 1;
            MovieCreateDto createdMovie = new MovieCreateDto
            {
                Title = title,
                Comment = comment,
                Rating = rating
            };

            var badRequest = _controller.CreateMovie(createdMovie);

            Assert.IsType<BadRequestResult>(badRequest.Result);
        }
        
        private void SetUpCorrectAuthTokenForController()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer " + GetMockJwtToken().Result;
        }

        private async Task<string> GetMockJwtToken()
        {
            var auth = await _userService.GetTokenAsync(new TokenRequestModel());
            return auth.Token;
        }
    }
}