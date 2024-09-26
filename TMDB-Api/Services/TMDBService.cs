using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using TMDB_Api.Configurations;
using TMDB_Api.Interfaces;
using TMDB_Api.Models;

namespace TMDB_Api.Services;

public class TMDBService : ITMDBService
{
    private readonly HttpClient _httpClient;
    private readonly TMDBConfig _config;

    private readonly JsonSerializerOptions _jsonOptions;


    public TMDBService(HttpClient httpClient, IOptions<TMDBConfig> config)
    {
        _httpClient = httpClient;
        _config = config.Value;
        _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ApiKey);

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
    }


    public async Task<List<Genre>> GetGenresAsync(string mediaType)
    {
        var response = await _httpClient.GetAsync($"genre/{mediaType}/list");
        Console.WriteLine(response);
        var content = await response.Content.ReadAsStringAsync();

        var genreResponse = JsonSerializer.Deserialize<GenreResponse>(content);
        Console.WriteLine(genreResponse.genres);
        return genreResponse.genres;
    }

    public async Task<PagedResult<MovieResult>> GetMoviesByGenreAsync(int genreId, int page)
    {
        var response = await _httpClient.GetAsync($"discover/movie?api_key={_config.ApiKey}&with_genres={genreId}&page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PagedResult<MovieResult>>(content);
    }

    public async Task<PagedResult<TvShowResult>> GetTVShowsByGenreAsync(int genreId, int page)
    {
        var response = await _httpClient.GetAsync($"discover/tv?api_key={_config.ApiKey}&with_genres={genreId}&page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PagedResult<TvShowResult>>(content);
    }

    public async Task<MovieResult> GetMovieDetailsAsync(int movieId)
    {
        var response = await _httpClient.GetAsync($"movie/{movieId}?api_key={_config.ApiKey}&append_to_response=credits");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MovieResult>(content, _jsonOptions);
    }

    public async Task<TvShowResult> GetTVShowDetailsAsync(int tvShowId)
    {
        var response = await _httpClient.GetAsync($"tv/{tvShowId}?api_key={_config.ApiKey}&append_to_response=credits");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TvShowResult>(content, _jsonOptions);
    }

    public async Task<PagedResult<MovieResult>> GetTrendingMoviesAsync()
    {
        var response = await _httpClient.GetAsync($"trending/movie/week?api_key={_config.ApiKey}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PagedResult<MovieResult>>(content, _jsonOptions);
    }

    public async Task<PagedResult<TvShowResult>> GetTrendingTVShowsAsync()
    {
        var response = await _httpClient.GetAsync($"trending/tv/week?api_key={_config.ApiKey}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PagedResult<TvShowResult>>(content, _jsonOptions);
    }
}
