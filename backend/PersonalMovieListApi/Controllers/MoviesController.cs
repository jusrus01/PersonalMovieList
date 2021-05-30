using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IMapper _mapper;

        public MoviesController(IMoviesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        //GET api/movies
        [HttpGet]
        public ActionResult <IEnumerable<Movie>> GetAllMovies()
        {
            IEnumerable<Movie> movies = _repo.GetAllMovies();
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        //GET api/movies/{id}
        [HttpGet("{id}", Name="GetMovieById")]
        public ActionResult <MovieReadDto> GetMovieById(int id)
        {
            var commandItem = _repo.GetMovieById(id);
            if(commandItem != null)
            {
                return Ok(_mapper.Map<MovieReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/movies
        [HttpPost]
        public ActionResult<MovieReadDto> CreateMovie(MovieCreateDto movieCreateDto)
        {
            Movie newMovie = _mapper.Map<Movie>(movieCreateDto);
            
            _repo.CreateMovie(newMovie);
            _repo.SaveChanges();

            MovieReadDto createdMovie = _mapper.Map<MovieReadDto>(newMovie);

            return CreatedAtRoute(nameof(GetMovieById), new { Id = createdMovie.Id },
                createdMovie);
        }

        //PUT api/movies/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            Movie foundMovie = _repo.GetMovieById(id);
            if(foundMovie == null)
            {
                return NotFound();
            }
            _mapper.Map(movieUpdateDto, foundMovie);

            _repo.UpdateMovie(foundMovie);
            _repo.SaveChanges();

            return NoContent();
        }

        //DELETE api/movies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            Movie foundMovie = _repo.GetMovieById(id);
            if(foundMovie == null)
            {
                return NotFound();
            }
            _repo.DeleteMovie(foundMovie);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}