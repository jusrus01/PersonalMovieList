using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PersonalMovieListApi.Data;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepo _repo;

        public MoviesController(IMoviesRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public ActionResult <IEnumerable<Movie>> GetAllMovies()
        {
            IEnumerable<Movie> movies = _repo.GetAllMovies();
            return Ok(movies);
        }
    }
}