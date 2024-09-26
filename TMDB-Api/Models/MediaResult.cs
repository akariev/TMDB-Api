using System.Collections.Generic;
using TMDB_Api.Models;
namespace TMDB_Api.Models;

public abstract class MediaResult
{
    public bool adult { get; set; }
    public string backdrop_path { get; set; }
    public List<int> genre_ids { get; set; }
    public int id { get; set; }
    public string original_language { get; set; }
    public string overview { get; set; }
    public double popularity { get; set; }
    public string poster_path { get; set; }
    public double vote_average { get; set; }
    public int vote_count { get; set; }

    public Credits credits { get; set; }

    public IEnumerable<Cast> Cast => credits?.cast;
    public IEnumerable<Crew> Directors => credits?.crew?.Where(c => c.job.ToLower() == "director");

}

public class Crew
{
    public int id { get; set; }
    public string name { get; set; }
    public string job { get; set; }
    public string department { get; set; }
    public string profile_path { get; set; }
}

public class Credits
{
    public List<Cast> cast { get; set; }
    public List<Crew> crew { get; set; }
}