using Microsoft.AspNetCore.Mvc;
using TMDB_Api.Interfaces;
using TMDB_Api.Services;

namespace TMDB_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly ITMDBService _tmdbService;
    private readonly CachingService _cachingService;

    public GenresController(ITMDBService tmdbService, CachingService cachingService)
    {
        _tmdbService = tmdbService;
        _cachingService = cachingService;
    }
    [HttpGet("genres")]
    public async Task<IActionResult> GetGenres()
    {
        var movieGenres = await _cachingService.GetOrSet("MovieGenres", () => _tmdbService.GetGenresAsync("movie"));
        var tvGenres = await _cachingService.GetOrSet("TVGenres", () => _tmdbService.GetGenresAsync("tv"));

        return Ok(new { MovieGenres = movieGenres, TVGenres = tvGenres });
    }


    [HttpGet("{genreId}/movies")]
    public async Task<IActionResult> GetMoviesByGenre(int genreId, [FromQuery] int page = 1)
    {
        var result = await _cachingService.GetOrSet($"MoviesByGenre_{genreId}_{page}", () => _tmdbService.GetMoviesByGenreAsync(genreId, page));
        return Ok(result);
    }

    [HttpGet("{genreId}/tvshows")]
    public async Task<IActionResult> GetTVShowsByGenre(int genreId, [FromQuery] int page = 1)
    {
        var result = await _cachingService.GetOrSet($"TVShowsByGenre_{genreId}_{page}", () => _tmdbService.GetTVShowsByGenreAsync(genreId, page));
        return Ok(result);
    }
}
