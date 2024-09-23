namespace OMDB_Movie_App.Models
{
	public class OmdbMovieDetailsResponse
	{
		public string Title { get; set; } = string.Empty;
		public string Year { get; set; } = string.Empty;
		public string ImdbID { get; set; } = string.Empty;
		public string Plot { get; set; } = string.Empty;
		public string Director { get; set; } = string.Empty;
		public string Actors { get; set; } = string.Empty;
		public string Poster { get; set; } = string.Empty;
		public string ImdbRating { get; set; } = string.Empty;
	}
}
