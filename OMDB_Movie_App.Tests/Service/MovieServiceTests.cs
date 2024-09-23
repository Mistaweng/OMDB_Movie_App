using Moq;
using OMDB_Movie_App.Models;
using OMDB_Movie_App.Repositories.Interfaces;
using OMDB_Movie_App.Services;

namespace OMDB_Movie_App.Tests.Service
{
	public class MovieServiceTests
	{
		private readonly Mock<IMovieRepository> _movieRepositoryMock;
		private readonly MovieService _movieService;

		public MovieServiceTests()
		{
			_movieRepositoryMock = new Mock<IMovieRepository>();
			_movieService = new MovieService(_movieRepositoryMock.Object);
		}

		// Test for successful search with results
		[Fact]
		public async Task SearchMoviesAsync_ReturnsMoviesList()
		{
			var movieTitle = "Batman";
			var movieSearchResults = new List<OmdbMovieSearchResult>
			{
				new OmdbMovieSearchResult { Title = "Batman Begins", Year = "2005" }
			};
			_movieRepositoryMock.Setup(r => r.SearchMoviesAsync(movieTitle))
				.ReturnsAsync(movieSearchResults);

			var result = await _movieService.SearchMoviesAsync(movieTitle);

			Assert.NotNull(result);
			Assert.Single(result);
			Assert.Equal("Batman Begins", result[0].Title);
		}

		// Test for empty search results
		[Fact]
		public async Task SearchMoviesAsync_ReturnsEmptyListWhenNoMoviesFound()
		{
			var movieTitle = "UnknownMovie";
			_movieRepositoryMock.Setup(r => r.SearchMoviesAsync(movieTitle))
				.ReturnsAsync(new List<OmdbMovieSearchResult>());

			var result = await _movieService.SearchMoviesAsync(movieTitle);

			Assert.NotNull(result);
			Assert.Empty(result);
		}

		// Test for successful retrieval of movie details
		[Fact]
		public async Task GetMovieDetailsAsync_ReturnsMovieDetails()
		{
			var imdbId = "tt0372784"; 
			var movieDetails = new OmdbMovieDetailsResponse { Title = "Batman Begins", Year = "2005" };
			_movieRepositoryMock.Setup(r => r.GetMovieDetailsAsync(imdbId))
				.ReturnsAsync(movieDetails);

			var result = await _movieService.GetMovieDetailsAsync(imdbId);

			Assert.NotNull(result);
			Assert.Equal("Batman Begins", result.Title);
		}

		// Test for movie not found
		[Fact]
		public async Task GetMovieDetailsAsync_ReturnsNullWhenMovieNotFound()
		{
			var imdbId = "invalidId";
			_movieRepositoryMock.Setup(r => r.GetMovieDetailsAsync(imdbId))
				.ReturnsAsync((OmdbMovieDetailsResponse)null);

			var result = await _movieService.GetMovieDetailsAsync(imdbId);

			Assert.Null(result);
		}
	}
}
