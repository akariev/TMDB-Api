using Microsoft.AspNetCore.Mvc;
using TMDB_Api.Interfaces;
using TMDB_Api.Services;

namespace TMDB_Api.Controllers;

// Controllers/MoviesController.cs
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ITMDBService _tmdbService;
    private readonly CachingService _cachingService;

    public MoviesController(ITMDBService tmdbService, CachingService cachingService)
    {
        _tmdbService = tmdbService;
        _cachingService = cachingService;
    }

    [HttpGet("{movieId}")]
    public async Task<IActionResult> GetMovieDetails(int movieId)
    {
        var movie = await _cachingService.GetOrSet($"Movie_{movieId}", () => _tmdbService.GetMovieDetailsAsync(movieId));
        return Ok(movie);
    }
}
