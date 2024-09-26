namespace TMDB_Api.Models;
public class MovieResult : MediaResult
{
    public string original_title { get; set; }
    public string release_date { get; set; }
    public string title { get; set; }
    public bool video { get; set; }
}
