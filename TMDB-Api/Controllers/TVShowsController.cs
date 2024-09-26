using Microsoft.AspNetCore.Mvc;
using TMDB_Api.Interfaces;
using TMDB_Api.Services;

namespace TMDB_Api.Controllers;

// Controllers/TVShowsController.cs
[ApiController]
[Route("api/[controller]")]
public class TVShowsController : ControllerBase
{
    private readonly ITMDBService _tmdbService;
    private readonly CachingService _cachingService;

    public TVShowsController(ITMDBService tmdbService, CachingService cachingService)
    {
        _tmdbService = tmdbService;
        _cachingService = cachingService;
    }

    [HttpGet("{tvShowId}")]
    public async Task<IActionResult> GetTVShowDetails(int tvShowId)
    {
        var tvShow = await _cachingService.GetOrSet($"TVShow_{tvShowId}", () => _tmdbService.GetTVShowDetailsAsync(tvShowId));
        return Ok(tvShow);
    }
}