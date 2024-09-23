using OMDB_Movie_App.Models;
using OMDB_Movie_App.Repositories.Interfaces;
using OMDB_Movie_App.Services.Interfaces;

namespace OMDB_Movie_App.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;

		public MovieService(IMovieRepository movieRepository)
		{
			_movieRepository = movieRepository;
		}

		public async Task<List<OmdbMovieSearchResult>> SearchMoviesAsync(string title)
		{
			return await _movieRepository.SearchMoviesAsync(title);
		}

		public async Task<OmdbMovieDetailsResponse> GetMovieDetailsAsync(string imdbId)
		{
			return await _movieRepository.GetMovieDetailsAsync(imdbId);
		}
	}
}
