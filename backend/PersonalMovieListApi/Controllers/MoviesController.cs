using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Dtos;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Controllers
{
    [Authorize]
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepo _repo;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        //GET api/movies
        [HttpGet]
        public ActionResult <IEnumerable<MovieReadDto>> GetAllMovies()
        {
            string username = RetrieveUsernameFromJwtAuthToken();

            if(username == null)
            {
                return BadRequest("No authorization header found");
            }

            return Ok(_mapper.Map<IEnumerable<MovieModel>, IEnumerable<MovieReadDto>>(_repo.GetAllMoviesByUserName(username)));
        }

        //GET api/movies/{id}
        [HttpGet("{id}", Name="GetMovieById")]
        public ActionResult <MovieReadDto> GetMovieById(int id)
        {
            var movie = _repo.GetMovieById(id);
            var user = RetrieveUsernameFromJwtAuthToken();

            if(user == null || movie.OwnerUsername != user)
            {
                return NotFound();
            }

            if(movie != null)
            {
                MovieReadDto readMovie = _mapper.Map<MovieReadDto>(movie);
                return Ok(readMovie);
            }

            return NotFound();
        }

        //POST api/movies
        [HttpPost]
        public ActionResult<MovieReadDto> CreateMovie(MovieCreateDto movieCreateDto)
        {
            if(movieCreateDto == null)
            {
                return BadRequest("Bad params");
            }

            MovieModel newMovie = _mapper.Map<MovieCreateDto, MovieModel>(movieCreateDto);
            string username = RetrieveUsernameFromJwtAuthToken();

            if(username == null)
            {
                return BadRequest();
            }

            newMovie.OwnerUsername = username;
            
            try
            {
                newMovie.Image = Convert.FromBase64String(movieCreateDto.ImageBase64);
            }
            catch(FormatException e)
            {
                return BadRequest(e.Message);
            }
            catch(ArgumentNullException)
            {
                newMovie.Image = null;
            }

            if(!TryValidateModel(newMovie))
            {
                return BadRequest("Image length greater than 256kb");
            }

            try
            {
                _repo.CreateMovie(newMovie);
            }
            catch(ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }

            _repo.SaveChanges();

            MovieReadDto createdMovie = _mapper.Map<MovieReadDto>(newMovie);

            return CreatedAtRoute(nameof(GetMovieById), new { Id = createdMovie.Id },
                createdMovie);
        }

        //PUT api/movies/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            MovieModel foundMovie = _repo.GetMovieById(id);
            string username = RetrieveUsernameFromJwtAuthToken();
            
            if(username == null)
            {
                return BadRequest("No authorization header found"); 
            }

            if(foundMovie == null)
            {
                return NotFound();
            }

            if(foundMovie.OwnerUsername != username)
            {
                return BadRequest("You do not have permission to delete this item");
            }

            _mapper.Map(movieUpdateDto, foundMovie);

            if(movieUpdateDto.ImageBase64 != null)
            {
                try
                {
                    foundMovie.Image = Convert.FromBase64String(movieUpdateDto.ImageBase64);
                }
                catch
                {
                    // do nothing
                }
            }

            _repo.UpdateMovie(foundMovie);
            _repo.SaveChanges();

            return NoContent();
        }

        //DELETE api/movies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            MovieModel foundMovie = _repo.GetMovieById(id);
            string username = RetrieveUsernameFromJwtAuthToken();

            if(username == null)
            {
                return BadRequest("Bad authentication header");
            }

            if(foundMovie == null)
            {
                return NotFound();
            }

            if(foundMovie.OwnerUsername != username)
            {
                return BadRequest("You do not have permission to delete this item");
            }
            
            _repo.DeleteMovie(foundMovie);
            _repo.SaveChanges();

            return NoContent();
        }

        private string RetrieveUsernameFromJwtAuthToken()
        {
            try
            {
                StringValues values;
                bool result = Request.Headers.TryGetValue("Authorization", out values);

                if(result)
                {
                    string token = values[0].Split(' ', System.StringSplitOptions.RemoveEmptyEntries)[1];

                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken securityToken = handler.ReadToken(token) as JwtSecurityToken;
                        
                    return securityToken.Subject;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}