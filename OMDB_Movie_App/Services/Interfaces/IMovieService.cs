using OMDB_Movie_App.Models;

namespace OMDB_Movie_App.Services.Interfaces
{
	public interface IMovieService
	{
		Task<List<OmdbMovieSearchResult>> SearchMoviesAsync(string title);
		Task<OmdbMovieDetailsResponse> GetMovieDetailsAsync(string imdbId);
	}
}
