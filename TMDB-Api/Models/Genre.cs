namespace TMDB_Api.Models;

public class Genre
{
    public int id { get; set; }
    public string name { get; set; }
}
public class GenreResponse
{
    public List<Genre> genres { get; set; }
}

public class GenreResult<T>
{
    public int GenreId { get; set; }
    public PagedResult<T> Results { get; set; }
}