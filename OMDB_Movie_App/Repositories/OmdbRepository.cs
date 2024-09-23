using OMDB_Movie_App.Models;
using OMDB_Movie_App.Repositories.Interfaces;
using OMDB_Movie_App.Helpers;
using Newtonsoft.Json;

namespace OMDB_Movie_App.Repositories
{
	public class OmdbRepository : IMovieRepository
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public OmdbRepository(HttpClientHelper httpClientHelper)
		{
			_httpClient = httpClientHelper.Client;
			_apiKey = httpClientHelper.ApiKey;
		}

		public async Task<List<OmdbMovieSearchResult>> SearchMoviesAsync(string title)
		{
			string apiUrl = $"http://www.omdbapi.com/?apikey={_apiKey}&s={title}";
			var response = await _httpClient.GetStringAsync(apiUrl);
			var searchResponse = JsonConvert.DeserializeObject<OmdbSearchResponse>(response); 
			return searchResponse?.Search ?? new List<OmdbMovieSearchResult>();
		}

		public async Task<OmdbMovieDetailsResponse> GetMovieDetailsAsync(string imdbId)
		{
			string apiUrl = $"http://www.omdbapi.com/?apikey={_apiKey}&i={imdbId}";
			var response = await _httpClient.GetStringAsync(apiUrl);
			return JsonConvert.DeserializeObject<OmdbMovieDetailsResponse>(response); 
		}
	}
}
