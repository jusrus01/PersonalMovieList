using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Data.Users;
using PersonalMovieListApi.Dtos;
using PersonalMovieListApi.Models;
using System.Linq;

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

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            try
            {
                if(model.Email.Where(c => c == '@').Count() != 1)
                {
                    return BadRequest();
                }

                var result = await _userService.RegisterAsync(model);

                if(result.AccountCreated)
                {
                    return Ok(result);
                }

                return Conflict(result);
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _userService.GetTokenAsync(model);
                return Ok(result);
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }
        }
    }
}