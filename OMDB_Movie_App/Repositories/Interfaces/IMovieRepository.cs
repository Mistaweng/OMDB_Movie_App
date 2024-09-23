using OMDB_Movie_App.Models;

namespace OMDB_Movie_App.Repositories.Interfaces
{
	public interface IMovieRepository
	{
		Task<List<OmdbMovieSearchResult>> SearchMoviesAsync(string title);
		Task<OmdbMovieDetailsResponse> GetMovieDetailsAsync(string imdbId);
	}
}
