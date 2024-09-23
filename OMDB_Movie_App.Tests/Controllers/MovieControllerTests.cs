using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OMDB_Movie_App.Controllers;
using OMDB_Movie_App.Models;
using OMDB_Movie_App.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OMDB_Movie_App.Tests.Controllers
{
	public class MovieControllerTests
	{
		private readonly Mock<IMovieService> _movieServiceMock;
		private readonly MovieController _controller;

		public MovieControllerTests()
		{
			_movieServiceMock = new Mock<IMovieService>();
			_controller = new MovieController(_movieServiceMock.Object);
		}

		[Fact]
		public async Task SearchMovies_ReturnsOkResult()
		{
			var searchQuery = "Batman";
			_movieServiceMock.Setup(s => s.SearchMoviesAsync(searchQuery))
				.ReturnsAsync(new List<OmdbMovieSearchResult>
				{
					new OmdbMovieSearchResult { Title = "Batman Begins", Year = "2005" }
				});

			var result = await _controller.SearchMovies(searchQuery);

			var okResult = Assert.IsType<OkObjectResult>(result);
			var movies = Assert.IsType<List<OmdbMovieSearchResult>>(okResult.Value);
			Assert.NotEmpty(movies);
		}

		[Fact]
		public async Task SearchMovies_NoResults_ReturnsNotFoundResult()
		{
			var searchQuery = "UnknownMovie";
			_movieServiceMock.Setup(s => s.SearchMoviesAsync(searchQuery))
				.ReturnsAsync(new List<OmdbMovieSearchResult>());

			var result = await _controller.SearchMovies(searchQuery);

			var okResult = Assert.IsType<OkObjectResult>(result);
			var movies = Assert.IsType<List<OmdbMovieSearchResult>>(okResult.Value);
			Assert.Empty(movies);
		}

		[Fact]
		public async Task GetMovieDetails_ValidId_ReturnsOkResult()
		{
			var imdbId = "tt0372784"; 
			_movieServiceMock.Setup(s => s.GetMovieDetailsAsync(imdbId))
				.ReturnsAsync(new OmdbMovieDetailsResponse { Title = "Batman Begins", Year = "2005" });

			var result = await _controller.GetMovieDetails(imdbId);

			var okResult = Assert.IsType<OkObjectResult>(result);
			var movieDetails = Assert.IsType<OmdbMovieDetailsResponse>(okResult.Value);
			Assert.Equal("Batman Begins", movieDetails.Title);
		}

		[Fact]
		public async Task GetMovieDetails_InvalidId_ReturnsNotFoundResult()
		{
			var imdbId = "invalidId";
			_movieServiceMock.Setup(s => s.GetMovieDetailsAsync(imdbId))
				.ReturnsAsync((OmdbMovieDetailsResponse)null);

			var result = await _controller.GetMovieDetails(imdbId);

			Assert.IsType<NotFoundResult>(result); 
		}
	}
}
