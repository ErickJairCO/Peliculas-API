using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Bussiness;
using Peliculas.Dtos;

namespace Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieBL _movieBL;

        public MoviesController(MovieBL movieBL)
        {
            _movieBL = movieBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieBL.GetMoviesAsync();

            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetById(int movieId)
        {
            var movie = await _movieBL.GetMovieAsync(movieId);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(MovieDtoIn movie)
        {
            int id;
            id = await _movieBL.AddAsync(movie);

            //objeto anonimo
            return Created("", new { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int movieId, MovieDtoIn movie)
        {
            await _movieBL.UpdateAsync(movieId, movie);

            return Accepted(new { Message = "Movie update" });
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteAsync(int movieId)
        {
            await _movieBL.DeleteAsync(movieId);

            return NoContent();
        }



    }
}
