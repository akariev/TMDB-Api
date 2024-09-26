using TMDB_Api.Models;

namespace TMDB_Api.Interfaces;

public interface ITMDBService
{
    Task<List<Genre>> GetGenresAsync(string mediaType);
    Task<PagedResult<MovieResult>> GetMoviesByGenreAsync(int genreId, int page);
    Task<PagedResult<TvShowResult>> GetTVShowsByGenreAsync(int genreId, int page);
    Task<MovieResult> GetMovieDetailsAsync(int movieId);
    Task<TvShowResult> GetTVShowDetailsAsync(int tvShowId);
    Task<PagedResult<MovieResult>> GetTrendingMoviesAsync();
    Task<PagedResult<TvShowResult>> GetTrendingTVShowsAsync();
}