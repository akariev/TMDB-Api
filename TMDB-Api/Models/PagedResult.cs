namespace TMDB_Api.Models;

public class PagedResult<T>
{
    public int page { get; set; }
    public List<T> results { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
}