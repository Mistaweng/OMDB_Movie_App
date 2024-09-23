using Microsoft.Extensions.Options;
using OMDB_Movie_App.Configurations;

namespace OMDB_Movie_App.Helpers
{
	public class HttpClientHelper
	{
		public HttpClient Client { get; }
		public string ApiKey { get; }

		public HttpClientHelper(IOptions<OmdbSettings> omdbSettings)
		{
			ApiKey = omdbSettings.Value.ApiKey;
			Client = new HttpClient();
		}
	}
}
