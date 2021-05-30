using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Data.Users;
using PersonalMovieListApi.Dtos;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}