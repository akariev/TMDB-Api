using Microsoft.AspNetCore.Mvc;
using TMDB_Api.Interfaces;
using TMDB_Api.Services;
using TMDB_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace TMDB_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ITMDBService _tmdbService;
        private readonly CachingService _cachingService;

        public HomeController(ITMDBService tmdbService, CachingService cachingService)
        {
            _tmdbService = tmdbService ?? throw new ArgumentNullException(nameof(tmdbService));
            _cachingService = cachingService ?? throw new ArgumentNullException(nameof(cachingService));
        }

        [HttpGet]
        public async Task<IActionResult> GetHomepageData()
        {
           try
    {
        string cacheKey = "HomepageData";

        var cachedResult = _cachingService.GetOrSet(cacheKey, async () =>
        {
            var movieGenresTask = _tmdbService.GetGenresAsync("movie");
            var tvGenresTask = _tmdbService.GetGenresAsync("tv");

            var trendingMoviesTask = _tmdbService.GetTrendingMoviesAsync();
            var trendingTVShowsTask = _tmdbService.GetTrendingTVShowsAsync();

            await Task.WhenAll(movieGenresTask, tvGenresTask, trendingMoviesTask, trendingTVShowsTask);

            var moviesByGenreTasks = movieGenresTask.Result.Select(genre =>
                _tmdbService.GetMoviesByGenreAsync(genre.id, 1)
                    .ContinueWith(task => new GenreResult<MovieResult>
                    {
                        GenreId = genre.id,
                        Results = task.Result
                    })
            );

            var tvShowsByGenreTasks = tvGenresTask.Result.Select(genre =>
                _tmdbService.GetTVShowsByGenreAsync(genre.id, 1)
                    .ContinueWith(task => new GenreResult<TvShowResult>
                    {
                        GenreId = genre.id,
                        Results = task.Result
                    })
            );

            await Task.WhenAll(moviesByGenreTasks);
            await Task.WhenAll(tvShowsByGenreTasks);

            return new
            {
                MovieGenres = movieGenresTask.Result,
                TvGenres = tvGenresTask.Result,
                TrendingMovies = trendingMoviesTask.Result.results,
                TrendingTVShows = trendingTVShowsTask.Result.results,
                MoviesByGenre = moviesByGenreTasks.Select(t => t.Result).ToList(),
                TVShowsByGenre = tvShowsByGenreTasks.Select(t => t.Result).ToList()
            };
        });

        return Ok(await cachedResult);
    }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}