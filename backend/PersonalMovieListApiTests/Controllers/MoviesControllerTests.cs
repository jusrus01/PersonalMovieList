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
        public void Get_WhenCalledWithAuthToken_ReturnsOnlySpecifiedUsersMovies()
        {
            SetUpCorrectAuthTokenForController();
            string testUsernameFromToken = "test2";
            var result = _controller.GetAllMovies();
            var retrievedMovies = GetObjectResult(result);
            bool allMoviesHaveSameOwner = true;

            foreach(Movie movie in retrievedMovies)
            {
                if(movie.OwnerUsername != testUsernameFromToken)
                {
                    allMoviesHaveSameOwner = false;
                    break;
                }
            }

            Assert.True(allMoviesHaveSameOwner);
        }

        [Fact]
        public void Get_WhenCalledWithAuthToken_ReturnsOnlySpecifiedUsersMoviesButNoneExist()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer " +
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJyYW5kb21Ub2tlblVzZXJuYW1lIiwianRpIjoiMmM0MDU0MDktNzhkOS00YmRmLTgxZWUtM2EyNzI3ZTlmODBlIiwiZW1haWwiOiJyYW5kb21Ub2tlblVzZXJuYW1lZXN0QHJhbmRvbVRva2VuVXNlcm5hbWUiLCJ1aWQiOiIyMmU4NWU0Mi00Nzk1LTRiYTYtOTlmOC02MmI0OGU1MTUwMTIiLCJyb2xlcyI6IlVzZXIiLCJleHAiOjE2MjI5MDI1NjQsImlzcyI6IlNlY3VyZUFwaSIsImF1ZCI6IlNlY3VyZUFwaVVzZXIifQ.zDPqv0H-OQm6mg2LPEphD9PqCK5mRrKoXSOt7xMiYgo";

            string testUsernameFromToken = "randomTokenUsername";
            var result = _controller.GetAllMovies();
            var retrievedMovies = GetObjectResult(result);

            int count = retrievedMovies.Count();

            Assert.Equal(count, 0);
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

        private IEnumerable<Movie> GetObjectResult(ActionResult<IEnumerable<Movie>> result)
        {
            if (result.Result != null)
                return (IEnumerable<Movie>)((ObjectResult)result.Result).Value;
            return result.Value;            
        }
    }
}