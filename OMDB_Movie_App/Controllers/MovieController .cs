using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMDB_Movie_App.Services.Interfaces;

namespace OMDB_Movie_App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : ControllerBase
	{
		private readonly IMovieService _movieService;

		public MovieController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[HttpGet("search")]
		public async Task<IActionResult> SearchMovies([FromQuery] string title)
		{
			var searchResults = await _movieService.SearchMoviesAsync(title);
			return Ok(searchResults);
		}

		[HttpGet("{imdbId}")]
		public async Task<IActionResult> GetMovieDetails(string imdbId)
		{
			var movieDetails = await _movieService.GetMovieDetailsAsync(imdbId);

			if (movieDetails == null)
			{
				return NotFound(); 
			}

			return Ok(movieDetails);
		}

	}
}
