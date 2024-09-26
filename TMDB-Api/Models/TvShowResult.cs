namespace TMDB_Api.Models;
public class TvShowResult : MediaResult
{
    public List<string> origin_country { get; set; }
    public string original_name { get; set; }
    public string first_air_date { get; set; }
    public string name { get; set; }
}