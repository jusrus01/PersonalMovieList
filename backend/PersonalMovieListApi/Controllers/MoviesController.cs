using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Dtos;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepo _repo;
        private IMapper _mapper;

        public MoviesController(IMoviesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult <IEnumerable<Movie>> GetAllMovies()
        {
            IEnumerable<Movie> movies = _repo.GetAllMovies();
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }
    }
}